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
            
            //校验是否保存默认打印机
            if(App.Current.Properties.ContainsKey("defaultPrinter"))
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
