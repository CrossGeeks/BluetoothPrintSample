using Lims.Phone.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
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
                    break;
                default:
                    page = new MainPage();
                    break;
            }
            App.Current.MainPage.Navigation.PushAsync(page, true);
            //string pagename = String.Format("///{0}",obj.ToString().Trim());
            //Shell.Current.GoToAsync(pagename);
        }
    }
}
