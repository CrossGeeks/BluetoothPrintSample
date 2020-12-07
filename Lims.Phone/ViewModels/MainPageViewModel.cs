using Lims.Phone.Views;
using Shiny.BluetoothLE;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace Lims.Phone.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        //命令响应事件
        ICommand tapCommand;
        //打印机设置需要的命令事件
        public ICommand GetDeviceListCommand { get; set; }
        public ICommand SetAdapterCommand { get; set; }
        public ICommand CheckPermissionsCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public MainPageViewModel()
        {
            tapCommand = new Command(OnTapped);
            
            //校验是否保存默认打印机
            if(App.Current.Properties.ContainsKey("defaultPrinter"))
            { 
            }
            else
            {
                //没有设置默认打印机
                GetDeviceListCommand = new Command(Services.BlueToothPrinter.GetDeviceList);
                SetAdapterCommand = new Command(async () => await Services.BlueToothPrinter.SetAdapter());
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
