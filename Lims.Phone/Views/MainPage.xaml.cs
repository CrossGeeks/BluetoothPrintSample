using Lims.Phone.Services;
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
            mainPageViewModel.PrintName = Properties.Get("defaultprinter");

            //是否登录标志
            string tmp = Properties.Get("IsLogin");
            tmp = tmp == string.Empty ? "false" : tmp;
            mainPageViewModel.IsLogin =  Boolean.Parse(tmp);

            //账号
            mainPageViewModel.Account = Properties.Get("Account");

            //公司名称
            mainPageViewModel.Company = Properties.Get("Company");

            //名称
            mainPageViewModel.Name = Properties.Get("Name");

            //日期
            mainPageViewModel.Date = Properties.Get("Date");
        }
    }
}