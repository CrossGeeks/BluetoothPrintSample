using Lims.Phone.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lims.Phone.Services.Waybill
{
    public static class WaybillService
    {
        /// <summary>
        /// WebService连接
        /// </summary>
        private static SkpServiceReference.ServiceSoapClient serviceSoapClient = new SkpServiceReference.ServiceSoapClient(SkpServiceReference.ServiceSoapClient.EndpointConfiguration.ServiceSoap);
        
        /// <summary>
        /// 从远程WebService获取新的运单号码
        /// </summary>
        /// <param name="company">公司名称</param>
        /// <param name="name">名称</param>
        /// <returns></returns>
        public static string GetWaybillNumber(string company, string name)
        {
            string result = serviceSoapClient.XD_NEW_NUM(company, name);
            string[] temp = result.Split('#');
            string[] temp1 = temp[1].Split('=');
            result = temp1[1].ToString().Trim();

            return result;
        }

        public static void SaveWaybill(ShippingViewModel shippingViewModel)
        {
            //保价费等与保价金额的千分之三
            decimal bjje = Convert.ToDecimal(shippingViewModel.GuaranteedAmount);
            decimal bjf = bjje * 3 / 1000;
            bjf = Math.Floor(bjf);
            //计算应收款，计算公式 应收款=代收金额+运费+保价费+手续费+提货费+工本费+垫付款+送货费
            decimal ysk = Math.Floor(Convert.ToDecimal(shippingViewModel.CollectionAmount) +
                                     Convert.ToDecimal(shippingViewModel.FreightRates) +
                                     bjf +
                                     0 +
                                     0 +
                                     0 +
                                     0 +
                                     Convert.ToDecimal(shippingViewModel.DeliveryFees));

            string ydinfo = String.Format(
                "运单号={0}&发货站点={1}&发站城市={2}&提货网点={3}&目的地={4}&所在地={5}&提货方式={6}&货物名称={7}&件数={8}&收货人={9}&收货人电话={10}&托运人={11}&托运人电话={12}&代收金额={13}&垫付款={14}&运费={15}&总运费={16}&欠返={17}&是否回单={18}&保价金额={19}&保价费={20}&手续费={21}&提货费={22}&送货费={23}&工本费={24}&应收款={25}&付款方式={26}&提付={27}&现付={28}&回付={29}&经办人={30}&托运日期={31}&到站城市={32}&中转方式={33}&条码号={34}&提货电话={35}&到站电话={36}&备注={37}&",
                shippingViewModel.WaybillNumber.ToString().Trim(),//{0}运单号码
                string.Empty,//{1}发货站点，现阶段没有数据
                string.Empty,//{2}发货城市，现阶段没有数据
                shippingViewModel.PickupLocations.Trim(),//{3}提货网点
                shippingViewModel.Destination.Trim(),//{4}目的地
                string.Empty,//{5}所在地，目前没有数据
                "自提",//{6}提货方式，目前没有数据，统一填写“自提”
                shippingViewModel.CargoName.Trim(),//{7}货物名称，有校验，此处不加容错
                shippingViewModel.NumberOfPieces.Trim(),//{8}件数，有校验，此处不容错
                shippingViewModel.ConsigneeName.Trim(),//{9}收货人,有校验，此处不做容错
                shippingViewModel.ConsigneePhone.Trim(),//{10}收货人电话，有校验，此处不做容错
                shippingViewModel.ShipperName.Trim(),//{11}托运人，有校验，此处不做容错
                shippingViewModel.ShipperPhone.Trim(),//{12}托运人电话，有校验，此处不错容错
                Convert.ToDecimal(shippingViewModel.CollectionAmount).ToString().Trim(),//{13}代收金额，非必填项
                string.Empty,//{14},垫付款，目前没有数据
                Convert.ToDecimal(shippingViewModel.FreightRates).ToString().Trim(),//{15},运费，有校验，此处不做作容错
                string.Empty,//{16},总运费，目前没有计算公司，填空处理
                Convert.ToDecimal(shippingViewModel.OwedBack).ToString().Trim(),//{17},欠返
                "否",//{18},是否回回单目前统一填否
                Convert.ToDecimal(shippingViewModel.GuaranteedAmount).ToString().Trim(),//{19}，保价金额
                bjf.ToString().Trim(),//{20},保价费，千分之三
                string.Empty,//{21,手续费，暂无数据支持}
                string.Empty,//{22},提货费，暂无数据
                Convert.ToDecimal(shippingViewModel.DeliveryFees).ToString().Trim(),//{23},送货费
                string.Empty,//{24},工本费，暂无数据
                ysk,//{25},应收款
                string.IsNullOrEmpty(shippingViewModel.PaymentMethods) ? "提付" : shippingViewModel.PaymentMethods.Trim(),//{26}付款方式,为空默认提付
                string.Empty,//{27},提付，暂无数据
                string.Empty,//{28},现付，暂无数据
                string.Empty,//{29},回付,暂无数据
                "测试员",//{30},经办人，暂时都写测试员
                DateTime.Now.Date.ToString("yyyy-MM-dd").Trim(),//{31},托运日期，取当前日期
                string.Empty,//{32},到站城市，暂无数据
                shippingViewModel.TransitMethod.ToString().Trim(),//"全程",//string.IsNullOrEmpty(shippingViewModel.TransitMethod.ToString().Trim())? "全程" : shippingViewModel.TransitMethod.Trim()/*,//{33},中转方式，默认全程
                string.Empty,//{34},条码号，暂无数据
                string.Empty,//{35},提货电话，暂无数据
                string.Empty,//{36},到站电话，暂无数据
                string.Empty//{37},备注,暂无数据
            );

            string result = serviceSoapClient.XD_New(shippingViewModel.Company, ydinfo);

        }
    }
}
