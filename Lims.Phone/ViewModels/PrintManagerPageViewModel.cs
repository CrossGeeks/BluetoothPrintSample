using Lims.Phone.Services;
using Shiny.BluetoothLE;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;

namespace Lims.Phone.ViewModels
{
    public class PrintManagerPageViewModel : INotifyPropertyChanged
    {
        public bool IsScanning{ get; set;}

        public ObservableCollection<IPeripheral> Peripherals { get; set; } = new ObservableCollection<IPeripheral>();

        public ICommand GetDeviceListCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        
        private IPeripheral _selectedPeripheral;
        public IPeripheral SelectedPeripheral
        {
            get { return _selectedPeripheral; }
            set
            {
                _selectedPeripheral = value;
                if (_selectedPeripheral != null)
                {
                    BlueToothPrinter.SelectedPeripheral = _selectedPeripheral;
                    Properties.Set("defaultprinter", _selectedPeripheral.Name);
                    Properties.Set("defaultprinteruuid", _selectedPeripheral.Uuid.ToString().Trim());
                    string msg = string.Format("已将名为 {0} 的蓝牙打印机设为默认打印机，请返回！！！", _selectedPeripheral.Name);
                    App.Current.MainPage.DisplayAlert("提示信息", msg, "确定");
                    App.Current.MainPage.Navigation.PopAsync(true);
                    Peripherals = null;
                }
            }
        }

        public PrintManagerPageViewModel()
        {
            GetDeviceListCommand = new Command(GetDeviceList);
        }

        private void GetDeviceList(object obj)
        {
            BlueToothPrinter.GetDeviceList();

            IsScanning = BlueToothPrinter.IsScanning;
            Peripherals = BlueToothPrinter.Peripherals;
        }
    }
}
