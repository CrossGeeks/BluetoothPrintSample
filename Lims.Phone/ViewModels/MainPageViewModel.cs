using Lims.Phone.Services;
using Lims.Phone.Views;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;

namespace Lims.Phone.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        private bool _islogin;
        /// <summary>
        /// 是否已登录标志位
        /// </summary>
        public bool IsLogin 
        {
            get { return _islogin; }
            set 
            {
                _islogin = value;
                OnPropertyChanged();
            }
        }

        private string _loginorlogout;
        /// <summary>
        /// 登录或登出页面名称
        /// </summary>
        public string LoginOrLogout 
        {
            get { return _loginorlogout; }
            set 
            {
                _loginorlogout = value;
                OnPropertyChanged();
            }
        }

        private string _fonticon;
        /// <summary>
        /// 登录或登出图标名
        /// </summary>
        public string FontIcon 
        {
            get { return _fonticon; }
            set 
            {
                _fonticon = value;
                OnPropertyChanged();
            }
        }

        private string _loginorlogouttext;
        /// <summary>
        /// 登录或登出文本
        /// </summary>
        public string LoginOrLogoutText 
        {
            get { return _loginorlogouttext; }
            set 
            {
                _loginorlogouttext = value;
                OnPropertyChanged();
            }
        }

        private string _account;
        /// <summary>
        /// 账号
        /// </summary>
        public string Account 
        {
            get { return _account; }
            set 
            {
                _account = value;
                OnPropertyChanged();
            }
        }

        private string _company;
        /// <summary>
        /// 公司名称
        /// </summary>
        public string Company 
        {
            get { return _company; }
            set 
            {
                _company = value;
                OnPropertyChanged();
            } 
        }

        private string _name;
        /// <summary>
        /// 名称
        /// </summary>
        public string Name 
        {
            get { return _name; }
            set 
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        private string _date;
        public string Date
        {
            get { return _date; }
            set
            {
                _date = value;
                OnPropertyChanged();
            }
        }

        private string _printname;
        /// <summary>
        /// 默认打印机名称
        /// </summary>
        public string PrintName 
        {
            get { return _printname; }
            set 
            {
                _printname = value;
                OnPropertyChanged();
            }
        }

        //命令响应事件
        readonly ICommand tapCommand;

        //打印机设置需要的命令事件
        public ICommand CheckPermissionsCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public MainPageViewModel()
        {
            tapCommand = new Command(OnTapped);
            //蓝牙设备检查调用，必须放在主页面，否则不起作用
            BlueToothPrinter.CheckPermissions();
        }

        public ICommand TapCommand
        {
            get { return tapCommand; }
        }

        private void OnTapped(object obj)
        {
            Page page;

            switch(obj.ToString().ToUpper().Trim())
            {
                case "SHIPPING":
                    page = new ShippingPage();
                    Application.Current.MainPage.Navigation.PushAsync(page, true);
                    break;
                case "LOGOUT":
                    IsLogin = false;
                    Account = string.Empty;
                    Company = string.Empty;
                    Name = string.Empty;
                    Date = string.Empty;
                    LoginOrLogout = "Login";
                    FontIcon = FontAwesome.FontAwesomeIcons.SignInAlt;
                    LoginOrLogoutText = "登录";

                    SaveToPropertitys.SaveTo(IsLogin, Account, Company, Name, Date);

                    Application.Current.MainPage.Navigation.PopToRootAsync(true);
                    break;
                case "LOGIN":
                    page = new LoginPage();
                    App.Current.MainPage.Navigation.PushAsync(page, true);
                    break;
                case "PRINTMANAGER":
                    page = new PrintManagerPage();
                    App.Current.MainPage.Navigation.PushAsync(page, true);
                    break;
                default:
                    App.Current.MainPage.Navigation.PopToRootAsync(true);
                    break;
            }
        }
    }
}
