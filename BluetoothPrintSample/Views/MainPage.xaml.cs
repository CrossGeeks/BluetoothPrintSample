using BluetoothPrintSample.ViewModels;
using Xamarin.Forms;

namespace BluetoothPrintSample.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainPageViewModel();
        }
    }
}
