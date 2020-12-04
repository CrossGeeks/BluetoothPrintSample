using Shiny.BluetoothLE;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Lims.Phone.ViewModels
{
    public class PrintPageViewModel: INotifyPropertyChanged
    {
        IDisposable _perifDisposable;
        IGattCharacteristic _savedCharacteristic;

        public bool IsReadyToPrint { get; set; }
        public string TextToPrint { get; set; } = "\n--------------------------------\n---------MyCompanyName--------\nProduct 1----------------34USD\nProduct 2----------------10USD\nTotal--------------------44USD";

        public ICommand PrintCommand { get; set; }
        public ICommand ConnectDeviceCommand { get; set; }

        public PrintPageViewModel(IPeripheral peripheral)
        {
            PrintCommand = new Command(PrintText);
            ConnectDeviceCommand = new Command<IPeripheral>(ConnectPrinter);
            ConnectDeviceCommand.Execute(peripheral);
        }

        void ConnectPrinter(IPeripheral selectedPeripheral)
        {
            if(!selectedPeripheral.IsConnected())
                selectedPeripheral.Connect();

            _perifDisposable = selectedPeripheral.WhenAnyCharacteristicDiscovered().Subscribe((characteristic) =>
            {
                //System.Diagnostics.Debug.WriteLine(characteristic.Description); //this is not suppported at this momment, and no neccesary I guess
                if (characteristic.CanWrite() && !characteristic.CanRead() && !characteristic.CanNotify())
                {
                    IsReadyToPrint = true;
                    _savedCharacteristic = characteristic;
                    System.Diagnostics.Debug.WriteLine($"Writing {characteristic.Uuid} - {characteristic.CanRead()} - {characteristic.CanIndicate()} - {characteristic.CanNotify()}");
                    _perifDisposable.Dispose();
                }
            });
        }
        
        void PrintText()
        {
            //设置标签纸尺寸，间隙尺寸，并且清空打印缓冲区
            string printstr = String.Format("SIZE {0} mm, {1} mm \r\n",74,56);
            printstr += String.Format("GAP {0} mm \r\n",2);
            printstr += String.Format("CLS \r\n");
            //第一行数据 名称及单据编号
            printstr += String.Format("TEXT {0},{1},\"{2}\",{3},{4},{5},\"{6}\" \r\n", 6, 24, "TSS32.BF2", 0, 1, 1, "胜京物流收货凭证");
            printstr += String.Format("TEXT {0},{1},\"{2}\",{3},{4},{5},\"{6}\" \r\n", 324, 24, "4", 0, 1, 1, "8542214");
            //画一条细线
            printstr += String.Format("BAR {0},{1},{2},{3} \r\n",2,66,580,1);
            //代收款及应收款
            printstr += String.Format("TEXT {0},{1},\"{2}\",{3},{4},{5},\"{6}\" \r\n",6,76, "TSS24.BF2", 0, 1, 1,"代收款：");
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
            printstr += String.Format("TEXT {0},{1},\"{2}\",{3},{4},{5},\"{6}\" \r\n",300,300, "TSS24.BF2",0,3,3,"胜");
            //打印二维码
            //printstr += String.Format("QRCODE {0},{1},\"{2}\",{3},\"{4}\",{5},\"{6}\" \r\n",400,390,"H",10,"A",0,"www.azure.cn");
            printstr += String.Format("QRCODE {0},{1},{2},{3},{4},{5},\"{6}\" \r\n",440,280,"H",4,"A",0,"https://www.azure.cn");
            //打印标签
            printstr += String.Format("PRINT {0} \r\n",1);

            //打印机指令包含中卫应为GB2312，装换之
            byte[] printbytes = Encoding.GetEncoding("UTF-8").GetBytes(printstr);
            printbytes = Encoding.Convert(Encoding.GetEncoding("UTF-8"), Encoding.GetEncoding("GB2312"), printbytes);
            MemoryStream stream = new MemoryStream(printbytes);

            //打印后续操作
            //Write(bs)     小文本打印方式
            _savedCharacteristic?.BlobWrite(stream).Subscribe(
                result => {
                    Debug.WriteLine(result.Position);
                },
                exception => {
                    ShowMessage("错误", "无法完成打印");
                }
            );
            ShowMessage("信息", "打印完毕");
        }

        void ShowMessage(string title, string message)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                await App.Current.MainPage.DisplayAlert(title, message, "确定");
            });
        }
        
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
