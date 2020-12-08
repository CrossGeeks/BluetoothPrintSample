using Lims.Phone.Views;
using Shiny.BluetoothLE;
using System;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace Lims.Phone.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// 是否已登录标志位
        /// </summary>
        public bool IsLogin { get; set; }

        /// <summary>
        /// 登录或登出页面名称
        /// </summary>
        public string LoginOrLogout { get; set; }

        /// <summary>
        /// 登录或登出图标名
        /// </summary>
        public string FontIcon { get; set; }

        /// <summary>
        /// 登录或登出文本
        /// </summary>
        public string LoginOrLogoutText { get; set; }

        private string UserName { get; set; }
        private string CompanyName { get; set; }
        private string PrintName { get; set; }

        //命令响应事件
        readonly ICommand tapCommand;

        //打印机设置需要的命令事件
        public ICommand CheckPermissionsCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public MainPageViewModel()
        {
            tapCommand = new Command(OnTapped);

            //检查登录标志
            if (App.Current.Properties.ContainsKey("IsLogin"))
            {
                if (Convert.ToBoolean(App.Current.Properties["IsLogin"]))
                {
                    //已登录
                    IsLogin = true;
                    LoginOrLogout = "Logout";
                    FontIcon = FontAwesome.FontAwesomeIcons.SignOutAlt;
                    LoginOrLogoutText = "退出登录";
                }
                else
                {
                    //未登录
                    IsLogin = false;
                    LoginOrLogout = "Login";
                    FontIcon = FontAwesome.FontAwesomeIcons.SignInAlt;
                    LoginOrLogoutText = "登录";
                }
            }
            else
            {
                //没有登录标志
                IsLogin = false;
                LoginOrLogout = "Login";
                FontIcon = FontAwesome.FontAwesomeIcons.SignInAlt;
                LoginOrLogoutText = "登录";
            }

            //校验是否保存默认打印机
            if (App.Current.Properties.ContainsKey("defaultPrinter"))
            {
                PrintName = App.Current.Properties["defaultPrinter"].ToString();
                Services.BlueToothPrinter.SetDefaultPrinter(PrintName);
            }
            else
            {
                //没有设置默认打印机
                CheckPermissionsCommand = new Command(async () => await Services.BlueToothPrinter.CheckPermissions());
                CheckPermissionsCommand.Execute(null);
            }
        }

        public ICommand TapCommand
        {
            get { return tapCommand; }
        }

        private void OnTapped(object obj)
        {
            Page page;

            switch(obj.ToString().Trim())
            {
                case "Login":
                    page = new LoginPage();
                    break;
                case "PrintManager":
                    page = new PrintManagerPage();
                    break;
                default:
                    page = new MainPage();
                    break;
            }

            App.Current.MainPage.Navigation.PushAsync(page, true);
        }
    }
}
