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

                message = validationResult.Errors[1].ErrorMessage;
                controllername = validationResult.Errors[1].PropertyName;
                Entry entry = (Entry)FindByName(controllername);
                DisplayAlert("错误信息",message,"确定");
                entry.Focus();
            }
        }
    }
}