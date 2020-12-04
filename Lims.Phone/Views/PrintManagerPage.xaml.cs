using Lims.Phone.ViewModels;
using Xamarin.Forms;

namespace Lims.Phone.Views
{
    public partial class PrintManagerPage : ContentPage
    {
        public PrintManagerPage()
        {
            InitializeComponent();
            BindingContext = new PrintManagerPageViewModel();
        }
    }
}
