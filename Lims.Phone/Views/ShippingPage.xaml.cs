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
        }
    }
}