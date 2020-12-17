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
        private static readonly IDisposable _scanDisposable;
        private static IDisposable _connectedDisposable;
        private static IBleManager _centralManager = Shiny.ShinyHost.Resolve<IBleManager>();

        public static ObservableCollection<IPeripheral> Peripherals { get; set; } = new ObservableCollection<IPeripheral>();

        public static void PrintWaybill(ShippingViewModel viewModel)
        {
            string guidName = Properties.Get("defaultprinter").ToString().Trim();
            var tt = Getperipheral(guidName);
        }
        private static IPeripheral Getperipheral(string printname)
        {
            IPeripheral peripheral = null;

            _connectedDisposable = _centralManager.GetConnectedPeripherals().Subscribe(scanResult =>
            {
                scanResult.ToList().ForEach(item =>
                {
                    if (!string.IsNullOrEmpty(item.Name) && item.Name == printname)
                    {
                        peripheral =  item;
                    }
                });
            });

            return peripheral;
        }
    }
}
