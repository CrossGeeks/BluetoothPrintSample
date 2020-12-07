using Shiny.BluetoothLE;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace Lims.Phone.ViewModels
{
    public class PrintManagerPageViewModel : INotifyPropertyChanged
    {
        public bool IsScanning { get; set; }
        public ObservableCollection<IPeripheral> Peripherals { get; set; } = new ObservableCollection<IPeripheral>();

        public ICommand GetDeviceListCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
      
        IPeripheral _selectedPeripheral;
        public IPeripheral SelectedPeripheral
        {
            get 
            {
                return _selectedPeripheral;
            }
            set 
            {
                _selectedPeripheral = value;
                if (_selectedPeripheral != null)
                {
                    Services.BlueToothPrinter.SelectedPeripheral = _selectedPeripheral;
                    string msg = string.Format("已将名为 {0} 的蓝牙打印机设为默认打印机，请返回！！！", _selectedPeripheral.Name);
                    App.Current.MainPage.DisplayAlert("提示信息",msg,"确定");
                    Peripherals = null;
                }
            }
        }

        public PrintManagerPageViewModel()
        {
        }
    }
}
