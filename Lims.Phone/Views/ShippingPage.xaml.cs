﻿using FluentValidation.Results;
using Lims.Phone.Services.Waybill;
using Lims.Phone.Services.Printer;
using Lims.Phone.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Lims.Phone.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShippingPage : ContentPage
    {
        public ShippingPage()
        {
            InitializeComponent();

            BindingContext = new ShippingViewModel();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            //viewModel
            ShippingViewModel viewModel = (ShippingViewModel)BindingContext;

            ShippingViewModelValidator validationRules = new ShippingViewModelValidator();
            ValidationResult validationResult = validationRules.Validate((ShippingViewModel)BindingContext);
            if (validationResult.IsValid)
            {
                //校验通过
                //取运单号
                viewModel.WaybillNumber = WaybillService.GetWaybillNumber(viewModel.Company, viewModel.Name);
                //保存运单
                if (WaybillService.SaveWaybill(viewModel))
                {
                    DisplayAlert("x信息", "运单保存完毕", "确定");
                    //将运单保存按钮状态改变，使打印按钮可用
                    SaveButton.IsEnabled = false;
                    viewModel.IsSave = true;
                }
            }
            else
            {
                //脚面校验未通过，需要显示错误
                string message = string.Empty;
                string controllername = string.Empty;

                controllername = validationResult.Errors[0].PropertyName;
                message = validationResult.Errors[0].ErrorMessage;

                var controller = FindByName(controllername);
                var controllertype = controller.GetType(); ;

                switch(controllertype.Name.ToUpper().Trim())
                {
                    case "PICKER":
                        Picker picker = (Picker)controller;
                        picker.Focus();
                        break;
                    default:
                        App.Current.MainPage.DisplayAlert("错误提示", message, "确定");
                        Entry entry = (Entry)controller;
                        entry.Focus();
                        break;
                }
            }
        }

        private void PaymentMethods_SelectedIndexChanged(object sender, EventArgs e)
        {
            Picker picker = (Picker)sender;
            ShippingViewModel viewModel = (ShippingViewModel)this.BindingContext;
            viewModel.PaymentMethods = picker.SelectedItem.ToString();
        }

        private void TransitMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            Picker picker = (Picker)sender;
            ShippingViewModel viewModel = (ShippingViewModel)this.BindingContext;
            viewModel.TransitMethod = picker.SelectedItem.ToString();
        }

        private async void Print_Button_ClickedAsync(object sender, EventArgs e)
        {
            //获取页面数据模型
            ShippingViewModel viewModel = (ShippingViewModel)this.BindingContext;

            //显示打印选项
            string action = await DisplayActionSheet("选择打印方式", "取消","", "运单单据", "全部标签", "部分标签");

            //分拣打印选项
            switch(action)
            {
                case "运单单据":
                    //await DisplayAlert("提示", "打印运单票据", "确定");
                    WaybillPrint.PrintWaybill(viewModel);
                    break;
                case "全部标签":
                    await DisplayAlert("提示", "打印全部标签", "确定");
                    break;
                case "部分标签":
                    await DisplayAlert("提示", "打印部分标签", "确定");
                    break;
                default:
                    break;
            }
        }
    }
}