using Lims.Phone.Services;
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
            PrintManagerPageViewModel viewModel = (PrintManagerPageViewModel)this.BindingContext;

            BlueToothPrinter.GetDeviceList();
            viewModel.IsScanning = BlueToothPrinter.IsScanning;
            if (viewModel.IsScanning)
            { 
                viewModel.Peripherals = BlueToothPrinter.Peripherals;
                BlueToothPrinter.IsScanning = false;
            }
        }
    }
}