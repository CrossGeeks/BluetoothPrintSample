using System;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Shiny.BluetoothLE;
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
                //System.Diagnostics.Debug.WriteLine(characteristic.Description); //this is not suppported at this momment, and no neccesary I guess
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
            //TEXT 100,100,"4",0,1,1,"DEMO FOR TEXT"
            //$"{TextToPrint}\n")
            //"CLS\r\n SELFTEST\r\n"
            _savedCharacteristic?.Write(Encoding.UTF8.GetBytes("SELFTEST\r\n")).Subscribe(
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
