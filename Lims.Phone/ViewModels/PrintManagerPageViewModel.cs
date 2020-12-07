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
                    Peripherals = null;
                    IsScanning = Services.BlueToothPrinter.IsScanning;
                }
            }
        }

        public PrintManagerPageViewModel()
        {
            //GetDeviceListCommand = new Command(Services.BlueToothPrinter.GetDeviceList);
            //IsScanning = false;
        }
    }
}
