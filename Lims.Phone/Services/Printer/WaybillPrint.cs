using Lims.Phone.ViewModels;
using Shiny.BluetoothLE;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Lims.Phone.Services.Printer
{
    public static class WaybillPrint
    {
        static IDisposable _perifDisposable;
        static IGattCharacteristic _savedCharacteristic;

        public static void PrintWaybill(ShippingViewModel viewModel)
        {
            IPeripheral peripheral = BlueToothPrinter.SelectedPeripheral;

            ConnectPrinter(peripheral);
            Task.Delay(10000);
            Print(viewModel);
        }

        private static void Print(ShippingViewModel viewModel)
        {
            string TemplateWaybill = GetTemplateWaybill("");
            string printstr = GetPrintString(TemplateWaybill, viewModel);

            //打印机指令包含中卫应为GB2312，转换
            byte[] printbytes = Encoding.GetEncoding("UTF-8").GetBytes(printstr);
            printbytes = Encoding.Convert(Encoding.GetEncoding("UTF-8"), Encoding.GetEncoding("GB2312"), printbytes);
            MemoryStream stream = new MemoryStream(printbytes);

            //打印后续操作
            _savedCharacteristic.BlobWrite(stream).Subscribe(
                result => {
                    Debug.WriteLine(result.Position + "/" + result.TotalLength);
                    //Debug.WriteLine(result.Position);
                },
                exception => {
                    App.Current.MainPage.DisplayAlert("错误", "无法完成打印", "确定");
                }
            );
        }

        private static string GetPrintString(string templateWaybill, ShippingViewModel viewModel)
        {
            string result = String.Format(templateWaybill,
                viewModel.Company.ToString().Trim(),                //0企业名称，Company
                viewModel.WaybillNumber.ToString().Trim(),          //1单据号码
                string.IsNullOrEmpty(viewModel.CollectionAmount) ? "" : viewModel.CollectionAmount.ToString().Trim(),   //2代收金额
                viewModel.AccountsReceivable.ToString().Trim(),     //3应收款
                DateTime.Now.Date.ToString("yyyy-MM-dd"),           //4据日期
                viewModel.CargoName.Trim(),                         //5货物名称
                "",                                                 //6货站点
                viewModel.Destination.Trim(),                       //7目的地
                viewModel.NumberOfPieces.Trim(),                    //8货物件数
                viewModel.ConsigneeName.Trim(),                     //9收货人姓名
                viewModel.ConsigneePhone.Trim(),                    //10收货人电话
                viewModel.ShipperName.Trim(),                       //11发货人姓名
                viewModel.ShipperPhone.Trim(),                      //12发货人电话
                viewModel.PaymentMethods.Trim(),                    //13付款方式
                viewModel.TotalShippingCost.Trim(),                 //14总运费
                "",                                                 //15备注
                viewModel.Name.Trim(),                              //16经办人
                viewModel.ShipperName.Trim(),                       //17发货人
                "",                                                 //18发站电话
                "",                                                  //19到站电话
                "",                                                  //20持卡人
                "",                                                  //21卡号
                "3",                                                 //22查询日期
                "胜",                                                //23Logo
                "我就是一个二维码，你可以扫扫看"                     //24二维码内容
                );

            return result;
        }

        /// <summary>
        /// {0}Company,{1}单据号，{2}代收款,{3}应收款，{4}date,{5}货品名称，{6}发货站点，{7}到货站点
        /// {8}件数，{9}收货人姓名，{10}收货电话，{11}托运人，{12}托运电话，{13}付款方式，{14}总运费
        /// {15}备注，{16}经办人,{17}发货人签字,{18}发站电话，{19}到站电话，{20}持卡人，{21}卡号
        /// {22}查询日期，{23}汉字Logo,{24}二维码内容
        /// </summary>
        /// <param name="template"></param>
        /// <returns></returns>
        private static string GetTemplateWaybill(string template)
        {
            //设置标签纸尺寸，间隙尺寸，并且清空打印缓冲区
            string printstr = "SIZE 74 mm, 56 mm \r\n";
            printstr += "GAP 2 mm \r\n";
            printstr += "CLS \r\n";
            //第一行数据 名称及单据编号
            printstr += "TEXT 6,24,\"TSS32.BF2\",0,1,1,\"{0}收货凭证\" \r\n";
            printstr += "TEXT 324,24,\"4\",0,1,1,\"{1}\" \r\n";
            //画一条细线
            printstr += "BAR 2,66,580,1 \r\n";
            //代收款及应收款
            printstr += "TEXT 6,76,\"TSS24.BF2\",0,1,1,\"代收款：\" \r\n";
            printstr += "TEXT 92,76,\"3\",0,1,1,\"{2}\" \r\n";
            printstr += "TEXT 180,76,\"TSS24.BF2\",0,1,1,\"应收款：\" \r\n";
            printstr += "TEXT 284,76,\"3\",0,1,1,\"{3}\" \r\n";
            //日期及货品
            printstr += "TEXT 6,106,\"TSS24.BF2\",0,1,1,\"日期：\" \r\n";
            printstr += "TEXT 76,110,\"2\",0,1,1,\"{4}\" \r\n";
            printstr += "TEXT 380,106,\"TSS24.BF2\",0,1,1,\"货品：\" \r\n";
            printstr += "TEXT 448,106,\"TSS24.BF2\",0,1,1,\"{5}\" \r\n";
            //发站、到站、件数
            printstr += "TEXT 6,136,\"TSS24.BF2\",0,1,1,\"发站：\" \r\n";
            printstr += "TEXT 76,136,\"TSS24.BF2\",0,1,1,\"{6}\" \r\n";
            printstr += "TEXT 180,136,\"TSS24.BF2\",0,1,1,\"到站：\" \r\n";
            printstr += "TEXT 248,136,\"TSS24.BF2\",0,1,1,\"{7}\" \r\n";
            printstr += "TEXT 380,136,\"TSS24.BF2\",0,1,1,\"件数：\" \r\n";
            printstr += "TEXT 448,136,\"3\",0,1,1,\"{8}\" \r\n";
            //收货人、收货电话
            printstr += "TEXT 6,166,\"TSS24.BF2\",0,1,1,\"收货人：\" \r\n";
            printstr += "TEXT 94,166,\"TSS24.BF2\",0,1,1,\"{9}\" \r\n";
            printstr += "TEXT 180,166,\"TSS24.BF2\",0,1,1,\"收货电话：\" \r\n";
            printstr += "TEXT 288,166,\"3\",0,1,1,\"{10}\" \r\n";
            //托运人、托运电话
            printstr += "TEXT 6,196,\"TSS24.BF2\",0,1,1,\"托运人：\" \r\n";
            printstr += "TEXT 94,196,\"TSS24.BF2\",0,1,1,\"{11}\" \r\n";
            printstr += "TEXT 180,196,\"TSS24.BF2\",0,1,1,\"托运电话：\" \r\n";
            printstr += "TEXT 288,196,\"3\",0,1,1,\"{12}\" \r\n";
            //付款方式、总运费、备注
            printstr += "TEXT 6,226,\"TSS24.BF2\",0,1,1,\"付款方式：\" \r\n";
            printstr += "TEXT 110,226,\"TSS24.BF2\",0,1,1,\"{13}\" \r\n";
            printstr += "TEXT 180,226,\"TSS24.BF2\",0,1,1,\"总运费：\" \r\n";
            printstr += "TEXT 268,226,\"3\",0,1,1,\"{14}\" \r\n";
            printstr += "TEXT 380,226,\"TSS24.BF2\",0,1,1,\"备注：\" \r\n";
            printstr += "TEXT 446,226,\"TSS24.BF2\",0,1,1,\"{15}\" \r\n";
            //分割线
            printstr += "BAR 0,260,580,1 \r\n";
            //经办人、发货人签字
            printstr += "TEXT 6,270,\"TSS24.BF2\",0,1,1,\"经办人：\" \r\n";
            printstr += "TEXT 94,270,\"TSS24.BF2\",0,1,1,\"{16}\" \r\n";
            printstr += "TEXT 180,270,\"TSS24.BF2\",0,1,1,\"发货人签字：\" \r\n";
            printstr += "TEXT 312,270,\"TSS24.BF2\",0,1,1,\"{17}\" \r\n";
            //发站电话
            printstr += "TEXT 6,300,\"TSS24.BF2\",0,1,1,\"发站电话：\" \r\n";
            printstr += "TEXT 110,300,\"3\",0,1,1,\"{18}\" \r\n";
            //到站电话
            printstr += "TEXT 6,330,\"TSS24.BF2\",0,1,1,\"到站电话：\" \r\n";
            printstr += "TEXT 110,330,\"3\",0,1,1,\"{19}\" \r\n";
            //发货存根
            printstr += "TEXT 6,360,\"TSS24.BF2\",0,1,1,\"发货存根：\" \r\n";
            printstr += "TEXT 110,360,\"TSS24.BF2\",0,1,1,\"保丢不保损！\" \r\n";
            //持卡人、卡号
            printstr += "TEXT 6,390,\"TSS24.BF2\",0,1,1,\"持卡人：\" \r\n";
            printstr += "TEXT 92,390,\"TSS24.BF2\",0,1,1,\"{20}\" \r\n";
            printstr += "TEXT 180,390,\"TSS24.BF2\",0,1,1,\"卡号：\" \r\n";
            printstr += "TEXT 252,393,\"3\",0,1,1,\"{21}\" \r\n";
            //提示信息
            printstr += "TEXT 6,420,\"TSS24.BF2\",0,1,1,\"货物查询时间为{22}日内，过期概不负责！\" \r\n";
            //LOGO字符打印
            printstr += "TEXT 300,300,\"TSS24.BF2\",0,3,3,\"{23}\" \r\n";
            //打印二维码
            printstr += "QRCODE 420,280,H,4,A,0,\"{24}\" \r\n";
            //打印命令
            printstr += "PRINT 1 \r\n";

            return printstr;
        }

        private static void ConnectPrinter(IPeripheral peripheral)
        {
            if (!peripheral.IsConnected())
            {
                peripheral.Connect();
            }

            peripheral.RequestMtu(512);
            _perifDisposable = peripheral.WhenAnyCharacteristicDiscovered().Subscribe((characteristic) =>
            {
                if (characteristic.CanWrite() && !characteristic.CanRead() && !characteristic.CanNotify())
                {
                    _savedCharacteristic = characteristic;
                    System.Diagnostics.Debug.WriteLine($"Writing {characteristic.Uuid} - {characteristic.CanRead()} - {characteristic.CanIndicate()} - {characteristic.CanNotify()}");
                    _perifDisposable.Dispose();
                }
            });
        }

        static void PrintText()
        {
            //设置标签纸尺寸，间隙尺寸，并且清空打印缓冲区
            string printstr = String.Format("SIZE {0} mm, {1} mm \r\n", 74, 56);
            printstr += String.Format("GAP {0} mm \r\n", 2);
            printstr += String.Format("CLS \r\n");
            //第一行数据 名称及单据编号
            printstr += String.Format("TEXT {0},{1},\"{2}\",{3},{4},{5},\"{6}\" \r\n", 6, 24, "TSS32.BF2", 0, 1, 1, "胜京物流收货凭证");
            printstr += String.Format("TEXT {0},{1},\"{2}\",{3},{4},{5},\"{6}\" \r\n", 324, 24, "4", 0, 1, 1, "8542214");
            //画一条细线
            printstr += String.Format("BAR {0},{1},{2},{3} \r\n", 2, 66, 580, 1);
            //代收款及应收款
            printstr += String.Format("TEXT {0},{1},\"{2}\",{3},{4},{5},\"{6}\" \r\n", 6, 76, "TSS24.BF2", 0, 1, 1, "代收款：");
            printstr += String.Format("TEXT {0},{1},\"{2}\",{3},{4},{5},\"{6}\" \r\n", 92, 76, "3", 0, 1, 1, "11.00");
            printstr += String.Format("TEXT {0},{1},\"{2}\",{3},{4},{5},\"{6}\" \r\n", 180, 76, "TSS24.BF2", 0, 1, 1, "应收款：");
            printstr += String.Format("TEXT {0},{1},\"{2}\",{3},{4},{5},\"{6}\" \r\n", 284, 76, "3", 0, 1, 1, "11.00");
            //日期及货品
            printstr += String.Format("TEXT {0},{1},\"{2}\",{3},{4},{5},\"{6}\" \r\n", 6, 106, "TSS24.BF2", 0, 1, 1, "日期：");
            printstr += String.Format("TEXT {0},{1},\"{2}\",{3},{4},{5},\"{6}\" \r\n", 76, 110, "2", 0, 1, 1, "2020-11-12 12:00");
            printstr += String.Format("TEXT {0},{1},\"{2}\",{3},{4},{5},\"{6}\" \r\n", 380, 106, "TSS24.BF2", 0, 1, 1, "货品：");
            printstr += String.Format("TEXT {0},{1},\"{2}\",{3},{4},{5},\"{6}\" \r\n", 448, 106, "TSS24.BF2", 0, 1, 1, "服装");
            //发站、到站、件数
            printstr += String.Format("TEXT {0},{1},\"{2}\",{3},{4},{5},\"{6}\" \r\n", 6, 136, "TSS24.BF2", 0, 1, 1, "发站：");
            printstr += String.Format("TEXT {0},{1},\"{2}\",{3},{4},{5},\"{6}\" \r\n", 76, 136, "TSS24.BF2", 0, 1, 1, "保定");
            printstr += String.Format("TEXT {0},{1},\"{2}\",{3},{4},{5},\"{6}\" \r\n", 180, 136, "TSS24.BF2", 0, 1, 1, "到站：");
            printstr += String.Format("TEXT {0},{1},\"{2}\",{3},{4},{5},\"{6}\" \r\n", 248, 136, "TSS24.BF2", 0, 1, 1, "北京");
            printstr += String.Format("TEXT {0},{1},\"{2}\",{3},{4},{5},\"{6}\" \r\n", 380, 136, "TSS24.BF2", 0, 1, 1, "件数：");
            printstr += String.Format("TEXT {0},{1},\"{2}\",{3},{4},{5},\"{6}\" \r\n", 448, 136, "3", 0, 1, 1, "1");
            //收货人、收货电话
            printstr += String.Format("TEXT {0},{1},\"{2}\",{3},{4},{5},\"{6}\" \r\n", 6, 166, "TSS24.BF2", 0, 1, 1, "收货人：");
            printstr += String.Format("TEXT {0},{1},\"{2}\",{3},{4},{5},\"{6}\" \r\n", 94, 166, "TSS24.BF2", 0, 1, 1, "测试");
            printstr += String.Format("TEXT {0},{1},\"{2}\",{3},{4},{5},\"{6}\" \r\n", 180, 166, "TSS24.BF2", 0, 1, 1, "收货电话：");
            printstr += String.Format("TEXT {0},{1},\"{2}\",{3},{4},{5},\"{6}\" \r\n", 288, 166, "3", 0, 1, 1, "15010201672");
            //托运人、托运电话
            printstr += String.Format("TEXT {0},{1},\"{2}\",{3},{4},{5},\"{6}\" \r\n", 6, 196, "TSS24.BF2", 0, 1, 1, "托运人：");
            printstr += String.Format("TEXT {0},{1},\"{2}\",{3},{4},{5},\"{6}\" \r\n", 94, 196, "TSS24.BF2", 0, 1, 1, "测试");
            printstr += String.Format("TEXT {0},{1},\"{2}\",{3},{4},{5},\"{6}\" \r\n", 180, 196, "TSS24.BF2", 0, 1, 1, "托运电话：");
            printstr += String.Format("TEXT {0},{1},\"{2}\",{3},{4},{5},\"{6}\" \r\n", 288, 196, "3", 0, 1, 1, "15010201672");
            //付款方式、总运费、备注
            printstr += String.Format("TEXT {0},{1},\"{2}\",{3},{4},{5},\"{6}\" \r\n", 6, 226, "TSS24.BF2", 0, 1, 1, "付款方式：");
            printstr += String.Format("TEXT {0},{1},\"{2}\",{3},{4},{5},\"{6}\" \r\n", 110, 226, "TSS24.BF2", 0, 1, 1, "提付");
            printstr += String.Format("TEXT {0},{1},\"{2}\",{3},{4},{5},\"{6}\" \r\n", 180, 226, "TSS24.BF2", 0, 1, 1, "总运费：");
            printstr += String.Format("TEXT {0},{1},\"{2}\",{3},{4},{5},\"{6}\" \r\n", 268, 226, "3", 0, 1, 1, "11.00");
            printstr += String.Format("TEXT {0},{1},\"{2}\",{3},{4},{5},\"{6}\" \r\n", 380, 226, "TSS24.BF2", 0, 1, 1, "备注：");
            printstr += String.Format("TEXT {0},{1},\"{2}\",{3},{4},{5},\"{6}\" \r\n", 446, 226, "3", 0, 1, 1, "15010201672");
            //分割线
            printstr += String.Format("BAR {0},{1},{2},{3} \r\n", 0, 260, 580, 1);
            //经办人、发货人签字
            printstr += String.Format("TEXT {0},{1},\"{2}\",{3},{4},{5},\"{6}\" \r\n", 6, 270, "TSS24.BF2", 0, 1, 1, "经办人：");
            printstr += String.Format("TEXT {0},{1},\"{2}\",{3},{4},{5},\"{6}\" \r\n", 94, 270, "TSS24.BF2", 0, 1, 1, "测试");
            printstr += String.Format("TEXT {0},{1},\"{2}\",{3},{4},{5},\"{6}\" \r\n", 180, 270, "TSS24.BF2", 0, 1, 1, "发货人签字：");
            printstr += String.Format("TEXT {0},{1},\"{2}\",{3},{4},{5},\"{6}\" \r\n", 312, 270, "TSS24.BF2", 0, 1, 1, "测试");
            //发站电话
            printstr += String.Format("TEXT {0},{1},\"{2}\",{3},{4},{5},\"{6}\" \r\n", 6, 300, "TSS24.BF2", 0, 1, 1, "发站电话：");
            printstr += String.Format("TEXT {0},{1},\"{2}\",{3},{4},{5},\"{6}\" \r\n", 110, 300, "3", 0, 1, 1, "15010201672");
            //发站电话
            printstr += String.Format("TEXT {0},{1},\"{2}\",{3},{4},{5},\"{6}\" \r\n", 6, 330, "TSS24.BF2", 0, 1, 1, "到站电话：");
            printstr += String.Format("TEXT {0},{1},\"{2}\",{3},{4},{5},\"{6}\" \r\n", 110, 330, "3", 0, 1, 1, "15010201672");
            //发货存根
            printstr += String.Format("TEXT {0},{1},\"{2}\",{3},{4},{5},\"{6}\" \r\n", 6, 360, "TSS24.BF2", 0, 1, 1, "发货存根：");
            printstr += String.Format("TEXT {0},{1},\"{2}\",{3},{4},{5},\"{6}\" \r\n", 110, 360, "TSS24.BF2", 0, 1, 1, "保丢不保损！");
            //持卡人、卡号
            printstr += String.Format("TEXT {0},{1},\"{2}\",{3},{4},{5},\"{6}\" \r\n", 6, 390, "TSS24.BF2", 0, 1, 1, "持卡人：");
            printstr += String.Format("TEXT {0},{1},\"{2}\",{3},{4},{5},\"{6}\" \r\n", 92, 390, "TSS24.BF2", 0, 1, 1, "测试");
            printstr += String.Format("TEXT {0},{1},\"{2}\",{3},{4},{5},\"{6}\" \r\n", 180, 390, "TSS24.BF2", 0, 1, 1, "卡号：");
            printstr += String.Format("TEXT {0},{1},\"{2}\",{3},{4},{5},\"{6}\" \r\n", 252, 393, "3", 0, 1, 1, "1234567890");
            //提示信息
            printstr += String.Format("TEXT {0},{1},\"{2}\",{3},{4},{5},\"{6}\" \r\n", 6, 420, "TSS24.BF2", 0, 1, 1, "货物查询时间为3日内，过期概不负责！");
            //LOGO字符打印
            printstr += String.Format("TEXT {0},{1},\"{2}\",{3},{4},{5},\"{6}\" \r\n", 300, 300, "TSS24.BF2", 0, 3, 3, "胜");
            //打印二维码
            //printstr += String.Format("QRCODE {0},{1},\"{2}\",{3},\"{4}\",{5},\"{6}\" \r\n",400,390,"H",10,"A",0,"www.azure.cn");
            printstr += String.Format("QRCODE {0},{1},{2},{3},{4},{5},\"{6}\" \r\n", 440, 280, "H", 4, "A", 0, "https://www.azure.cn");
            //打印标签
            printstr += String.Format("PRINT {0} \r\n", 1);

            //打印机指令包含中卫应为GB2312，装换之
            byte[] printbytes = Encoding.GetEncoding("UTF-8").GetBytes(printstr);
            printbytes = Encoding.Convert(Encoding.GetEncoding("UTF-8"), Encoding.GetEncoding("GB2312"), printbytes);
            MemoryStream stream = new MemoryStream(printbytes);

            //打印后续操作
            //Write(bs)     小文本打印方式
            _savedCharacteristic.BlobWrite(stream).Subscribe(
                result => {
                    Debug.WriteLine(result.Position);
                    Application.Current.MainPage.DisplayAlert("信息", "打印完毕", "确定");
                },
                exception => {
                    App.Current.MainPage.DisplayAlert("错误", "无法完成打印","确定");
                }
            );
        }
    }
}
