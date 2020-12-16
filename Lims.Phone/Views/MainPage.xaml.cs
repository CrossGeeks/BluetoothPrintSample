using Lims.Phone.ViewModels;
using System;
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
            //获取Main页ViewModel
            MainPageViewModel mainPageViewModel = (MainPageViewModel)this.BindingContext;
            
            //将内存字典值传递到ViewModel
            PropertiesToViewModel(mainPageViewModel);

            //状态修改
            ViewModelChange(mainPageViewModel);

            base.OnAppearing();
        }

        private void ViewModelChange(MainPageViewModel mainPageViewModel)
        {
            if(mainPageViewModel.IsLogin)
            {
                //已登录
                mainPageViewModel.LoginOrLogout = "Logout";
                mainPageViewModel.FontIcon = FontAwesome.FontAwesomeIcons.SignOutAlt;
                mainPageViewModel.LoginOrLogoutText = "退出";
            }
            else
            {
                //未登录
                mainPageViewModel.LoginOrLogout = "Login";
                mainPageViewModel.FontIcon = FontAwesome.FontAwesomeIcons.SignInAlt;
                mainPageViewModel.LoginOrLogoutText = "登录";
            }
        }

        private void PropertiesToViewModel(MainPageViewModel mainPageViewModel)
        {
            //检验打印机名字
            if (App.Current.Properties.ContainsKey("defaultPrinter"))
                mainPageViewModel.PrintName = App.Current.Properties["defaultPrinter"].ToString().Trim();

            //是否登录标志
            if (App.Current.Properties.ContainsKey("IsLogin".ToUpper().Trim()))
                mainPageViewModel.IsLogin = (bool)Application.Current.Properties["IsLogin".ToUpper().Trim()];

            //账号
            if (App.Current.Properties.ContainsKey("Account".ToUpper().Trim()))
                mainPageViewModel.Account = Application.Current.Properties["Account".ToUpper().Trim()].ToString().Trim();

            //公司名称
            if (App.Current.Properties.ContainsKey("Company".ToUpper().Trim()))
                mainPageViewModel.Company = Application.Current.Properties["Company".ToUpper().Trim()].ToString().Trim();

            //名称
            if (App.Current.Properties.ContainsKey("Name".ToUpper().Trim()))
                mainPageViewModel.Name = Application.Current.Properties["Name".ToUpper().Trim()].ToString().Trim();

            //日期
            if (App.Current.Properties.ContainsKey("Date".ToUpper().Trim()))
                mainPageViewModel.Date = Application.Current.Properties["Date".ToUpper().Trim()].ToString().Trim();
        }
    }
}