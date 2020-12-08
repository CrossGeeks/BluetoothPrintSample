using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Lims.Phone.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// 是否登录成功标志位
        /// </summary>
        public bool IsLogin { get; set; }

        /// <summary>
        /// 登录用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 登录密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 公司代码
        /// </summary>
        public string CompanyCode { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public LoginViewModel()
        {
        } 
    }
}
