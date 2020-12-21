using Lims.Phone.ViewModels;
using Shiny.BluetoothLE;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Lims.Phone.Services.Printer
{
    public static class WaybillPrint
    {
        //private static readonly IDisposable _scanDisposable;
        //private static IDisposable _connectedDisposable;
        //private static IBleManager _centralManager = Shiny.ShinyHost.Resolve<IBleManager>();

        //public static ObservableCollection<IPeripheral> Peripherals { get; set; } = new ObservableCollection<IPeripheral>();

        public static void PrintWaybill(ShippingViewModel viewModel)
        {
            IPeripheral peripheral = BlueToothPrinter.SelectedPeripheral;
            //string printername = Properties.Get("defaultprinter").ToString().Trim();
            //Print_Check(printername);
        }

        private static void Print_Check(string printername)
        {
            Services.BlueToothPrinter.CheckPermissions();
            //IPeripheral peripheral = BlueToothPrinter.GetDeviceByName(printername);
            BlueToothPrinter.GetDeviceList();
        }
    }
}
