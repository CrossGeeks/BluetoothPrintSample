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
    }
}