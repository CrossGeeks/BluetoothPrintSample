using Lims.Phone.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Lims.Phone.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            BindingContext = new MainPageViewModel();
        }
        protected override void OnAppearing()
        {
            DisplayAlert("提示信息", "我是主页面马上要出现了", "确定");
            base.OnAppearing();
        }
    }
}