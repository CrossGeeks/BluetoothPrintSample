using Xamarin.Forms;

namespace Lims.Phone.Services
{
    public static class SaveToPropertitys
    {
        /// <summary>
        /// 是否登录标志位
        /// </summary>
        public static string islogin = "islogin".ToUpper().Trim();

        /// <summary>
        /// 账号
        /// </summary>
        public static string account = "account".ToUpper().Trim();

        /// <summary>
        /// 公司名称
        /// </summary>
        public static string company = "company".ToUpper().Trim();

        /// <summary>
        /// 姓名
        /// </summary>
        public static string name = "name".ToUpper().Trim();

        /// <summary>
        /// 日期
        /// </summary>
        public static string date = "Date".ToUpper().Trim();

        /// <summary>
        /// 打印机名称
        /// </summary>
        public static string printername = "printername".ToUpper().Trim();
        
        /// <summary>
        /// 将信息保存到配置字典中
        /// </summary>
        /// <param name="IsLogin">是否登录</param>
        /// <param name="Account">账号</param>
        /// <param name="Company">公司名称</param>
        /// <param name="Name">姓名</param>
        /// <param name="Date">日期</param>
        public static void SaveTo(bool IsLogin = false, string Account = "", string Company = "", string Name = "", string Date = "")
        {
            if (Application.Current.Properties.ContainsKey(islogin))
                Application.Current.Properties[islogin] = IsLogin;
            else
                Application.Current.Properties.Add(islogin, IsLogin);

            //账号信息
            if (Application.Current.Properties.ContainsKey(account))
                Application.Current.Properties[account] = Account;
            else
                Application.Current.Properties.Add(account, Account);

            //公司名称
            if (Application.Current.Properties.ContainsKey(company))
                Application.Current.Properties[company] = Company;
            else
                Application.Current.Properties.Add(company, Company);

            //Name
            if (Application.Current.Properties.ContainsKey(name))
                Application.Current.Properties[name] = Name;
            else
                Application.Current.Properties.Add(name, Name);

            //Date
            if (Application.Current.Properties.ContainsKey(date))
                Application.Current.Properties[date] = Date;
            else
                Application.Current.Properties.Add(date, Date);

            Application.Current.SavePropertiesAsync();
        }
    }
}
