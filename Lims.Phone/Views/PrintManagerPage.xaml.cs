using Lims.Phone.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Lims.Phone.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PrintManagerPage : ContentPage
    {
        public PrintManagerPage()
        {
            InitializeComponent();

            BindingContext = new PrintManagerPageViewModel();
        }

        private void Button_Clicked(object sender, System.EventArgs e)
        {
            Services.BlueToothPrinter.GetDeviceList();

            PrintManagerPageViewModel vmodel = (PrintManagerPageViewModel)this.BindingContext;
            vmodel.IsScanning = Services.BlueToothPrinter.IsScanning;
            if(vmodel.IsScanning)
            { 
                vmodel.Peripherals = Services.BlueToothPrinter.Peripherals;
            }
        }
    }
}