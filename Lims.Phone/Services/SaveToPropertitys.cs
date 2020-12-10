using Xamarin.Forms;

namespace Lims.Phone.Services
{
    public static class SaveToPropertitys
    {
        public static void SaveTo(bool IsLogin = false, string Account = "", string Company = "", string Name = "", string Date = "")
        {
            if (Application.Current.Properties.ContainsKey("IsLogin"))
                Application.Current.Properties["IsLogin"] = IsLogin;
            else
                Application.Current.Properties.Add("IsLogin", IsLogin);

            //账号信息
            if (Application.Current.Properties.ContainsKey("Account"))
                Application.Current.Properties["Account"] = Account;
            else
                Application.Current.Properties.Add("Account", Account);

            //公司名称
            if (Application.Current.Properties.ContainsKey("Company"))
                Application.Current.Properties["Company"] = Company;
            else
                Application.Current.Properties.Add("Company", Company);

            //Name
            if (Application.Current.Properties.ContainsKey("Name"))
                Application.Current.Properties["Name"] = Name;
            else
                Application.Current.Properties.Add("Name", Name);

            //Date
            if (Application.Current.Properties.ContainsKey("Date"))
                Application.Current.Properties["Date"] = Date;
            else
                Application.Current.Properties.Add("Date", Date);

            Application.Current.SavePropertiesAsync();
        }
    }
}
