using FluentValidation.Results;
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
            ShippingViewModelValidator validationRules = new ShippingViewModelValidator();
            ValidationResult validationResult = validationRules.Validate((ShippingViewModel)BindingContext);
            if (validationResult.IsValid)
            {
                //校验通过
                //取运单号
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
    }
}