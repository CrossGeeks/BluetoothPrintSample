using System;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Shiny.BluetoothLE.Central;
using Xamarin.Forms;

namespace BluetoothPrintSample.ViewModels
{
    public class PrintPageViewModel: INotifyPropertyChanged
    {
        IDisposable _perifDisposable;
        IGattCharacteristic _savedCharacteristic;

        public bool IsReadyToPrint { get; set; }
        public string TextToPrint { get; set; } = "\n--------------------------------\n---------MyCompanyName--------\nProduct 1----------------34USD\nProduct 2----------------10USD\nTotal--------------------44USD";

        public ICommand PrintCommand { get; set; }
        public ICommand ConnectDeviceCommand { get; set; }

        public PrintPageViewModel(IPeripheral peripheral)
        {
            PrintCommand = new Command(PrintText);
            ConnectDeviceCommand = new Command<IPeripheral>(ConnectPrinter);
            ConnectDeviceCommand.Execute(peripheral);
        }

        void ConnectPrinter(IPeripheral selectedPeripheral)
        {
            if(!selectedPeripheral.IsConnected())
                selectedPeripheral.Connect();

            _perifDisposable = selectedPeripheral.WhenAnyCharacteristicDiscovered().Subscribe((characteristic) =>
            {
                System.Diagnostics.Debug.WriteLine(characteristic.Description);
                if (characteristic.CanWrite() && !characteristic.CanRead() && !characteristic.CanNotify())
                {
                    IsReadyToPrint = true;
                    _savedCharacteristic = characteristic;
                    System.Diagnostics.Debug.WriteLine($"Writing {characteristic.Uuid} - {characteristic.CanRead()} - {characteristic.CanIndicate()} - {characteristic.CanNotify()}");
                    _perifDisposable.Dispose();
                }
            });
        }
        
        void PrintText()
        {
            _savedCharacteristic?.Write(Encoding.UTF8.GetBytes($"{TextToPrint}\n")).Subscribe(
                result => {
                    ShowMessage("Yayy", "Data Printed");
                },
                exception => {
                    ShowMessage("Error", "Unable to print");
             });
        }

        void ShowMessage(string title, string message)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                await App.Current.MainPage.DisplayAlert(title, message, "Ok");
            });
        }
        public event PropertyChangedEventHandler PropertyChanged;

    }
}
