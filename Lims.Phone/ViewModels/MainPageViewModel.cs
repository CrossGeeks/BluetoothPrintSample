using Lims.Phone.Views;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace Lims.Phone.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        //命令响应事件
        ICommand tapCommand;
        public event PropertyChangedEventHandler PropertyChanged;

        public MainPageViewModel()
        {
            tapCommand = new Command(OnTapped);
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
                    App.Current.MainPage = page;
                    break;
                default:
                    page = new MainPage();
                    App.Current.MainPage.Navigation.PushAsync(page, true);
                    break;
            }
        }
    }
}
