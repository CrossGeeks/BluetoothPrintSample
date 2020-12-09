using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace Lims.Phone.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private bool _islogin;
        /// <summary>
        /// 是否登录成功标志位
        /// </summary>
        public bool IsLogin
        {
            get { return _islogin; }
            set
            {
                _islogin = value;
                OnPropertyChanged();
                //OnPropertyChanged(nameof(Message));
            }
        }

        private string _username;
        /// <summary>
        /// 登录用户名
        /// </summary>
        public string UserName 
        {
            get { return _username; }
            set 
            {
                _username = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Message));
            }
        }

        private string _password;
        /// <summary>
        /// 登录密码
        /// </summary>
        public string Password 
        {
            get { return _password; }
            set 
            {
                _password = value;
                OnPropertyChanged();
            }
        }

        private string _companycode;
        /// <summary>
        /// 公司代码
        /// </summary>
        public string CompanyCode 
        {
            get { return _companycode; }
            set 
            {
                _companycode = value;
                OnPropertyChanged();
            }
        }

        private bool _isautologin = false;
        public bool IsAutoLogin 
        {
            get { return _isautologin; }
            set 
            {
                _isautologin = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Message));
            }
        }
        
        /// <summary>
        /// 信息
        /// </summary>
        public string Message
        {
            get 
            {
                var msg = IsAutoLogin ? "下次将会自动登录" : "下次不会自动登录";

                return $"欢迎{UserName}.{msg}";
            }
        }

        private bool _isbusy;
        /// <summary>
        /// 是否处于忙碌状态
        /// </summary>
        public bool IsBusy
        {
            get { return _isbusy; }
            set 
            {
                _isbusy = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public Command LoginCommand { get; }
        public void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this,new PropertyChangedEventArgs(name));
        }

        public LoginViewModel()
        {
        }
    }
}
