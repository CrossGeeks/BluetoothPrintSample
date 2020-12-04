using Lims.Phone.ViewModels;
using Shiny.BluetoothLE;
using Xamarin.Forms;

namespace Lims.Phone.Views
{
    public partial class PrintPage : ContentPage
    {
        public PrintPage(IPeripheral peripheral)
        {
            InitializeComponent();
            BindingContext = new PrintPageViewModel(peripheral);
        }
    }
}
