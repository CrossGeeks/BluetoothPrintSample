﻿using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Shiny.BluetoothLE;
using Shiny;
using System.Reactive.Linq;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Lims.Phone.Views;
using System.Linq;

namespace Lims.Phone.ViewModels
{
    public class PrintManagerPageViewModel : INotifyPropertyChanged
    {
        //IDisposable _scanDisposable, _connectedDisposable;
        //IBleManager _centralManager = Shiny.ShinyHost.Resolve<IBleManager>();

        public bool IsScanning { get; set; }
        public ObservableCollection<IPeripheral> Peripherals { get; set; } = new ObservableCollection<IPeripheral>();

        public ICommand GetDeviceListCommand { get; set; }
        //ICommand SetAdapterCommand { get; set; }
        //ICommand CheckPermissionsCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        /*
        IPeripheral _selectedPeripheral;
        public IPeripheral SelectedPeripheral
        {
            get {
                return _selectedPeripheral;
            }
            set {
                _selectedPeripheral = value;
                if (_selectedPeripheral != null)
                {
                    App.Current.Properties.Add("defaultPrinter", _selectedPeripheral.Name);
                    Application.Current.SavePropertiesAsync(); 
                    OnSelectedPeripheral(_selectedPeripheral); }
            }
        }
        */
        public PrintManagerPageViewModel()
        {
            GetDeviceListCommand = new Command(Services.BlueToothPrinter.GetDeviceList);
            //SetAdapterCommand = new Command(async () => await Services.BlueToothPrinter.SetAdapter());
            //CheckPermissionsCommand = new Command(async () => await Services.BlueToothPrinter.CheckPermissions());

            //IsScanning = Services.BlueToothPrinter.IsScanning;
            /*
            if (App.Current.Properties.ContainsKey("defaultPrinter"))
            {
                string guidName = App.Current.Properties["defaultPrinter"].ToString();
                _connectedDisposable = _centralManager.GetConnectedPeripherals().Subscribe(scanResult =>
                {
                    scanResult.ToList().ForEach(
                     item =>
                     {
                         if (!string.IsNullOrEmpty(item.Name) && item.Name == guidName)
                         {
                             Peripherals.Add(item);
                             _centralManager.StopScan();
                             Device.BeginInvokeOnMainThread(async () =>
                             {
                                 await App.Current.MainPage.Navigation.PushAsync(new PrintPage(item));
                                 SelectedPeripheral = null;
                             });

                             _scanDisposable?.Dispose();
                             IsScanning = _centralManager.IsScanning;
                         }
                     });

                    _connectedDisposable?.Dispose();
                });

                if (_centralManager.IsScanning)
                    _centralManager.StopScan();
                if (_centralManager.Status == Shiny.AccessState.Available && !_centralManager.IsScanning)
                {
                    _scanDisposable = _centralManager.ScanForUniquePeripherals().Subscribe(scanResult =>
                    {
                        if (!string.IsNullOrEmpty(scanResult.Name) && !Peripherals.Contains(scanResult))
                        {
                            Peripherals.Add(scanResult);
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                await App.Current.MainPage.Navigation.PushAsync(new PrintPage(scanResult));
                                SelectedPeripheral = null;
                            });
                        }

                    });
                }
            }
            else
            {
                GetDeviceListCommand = new Command(GetDeviceList);
                SetAdapterCommand = new Command(async () => await SetAdapter());
                CheckPermissionsCommand = new Command(async () => await CheckPermissions());
                CheckPermissionsCommand.Execute(null);
            }*/
        }
        /*
        async Task CheckPermissions()
        {
            var status = await _centralManager.RequestAccess();
            if (status == AccessState.Denied)
            {
                await App.Current.MainPage.DisplayAlert("Permissions", "You need to have your bluetooth ON to use this app", "Ok");
                Xamarin.Essentials.AppInfo.ShowSettingsUI();
            }
            else
            {
                SetAdapterCommand.Execute(null);
            }
        }

        async Task SetAdapter()
        {
            var poweredOn = _centralManager.Status == AccessState.Available;
            if (!poweredOn && !_centralManager.TrySetAdapterState(true))
                await App.Current.MainPage.DisplayAlert("Cannot change bluetooth adapter state", "", "Ok");

            if (poweredOn)
            {
                GetConnectedDevices();
            }
        }
        */
        /*
        void GetConnectedDevices()
        {
            _connectedDisposable = _centralManager.GetConnectedPeripherals().Subscribe(scanResult =>
            {
                scanResult.ToList().ForEach(
                 item =>
                 {
                     if (!string.IsNullOrEmpty(item.Name))
                         Peripherals.Add(item);
                 });

                _connectedDisposable?.Dispose();
            });

            if (_centralManager.IsScanning)
                _centralManager.StopScan();
        }

        void OnSelectedPeripheral(IPeripheral peripheral)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                await App.Current.MainPage.Navigation.PushAsync(new PrintPage(peripheral));
                SelectedPeripheral = null;
            });

            _scanDisposable?.Dispose();
            IsScanning = _centralManager.IsScanning;
        }
        */
        /*
        void GetDeviceList()
        {
            if (_centralManager.IsScanning)
            {
                _scanDisposable?.Dispose();
            }
            else
            {
                if (_centralManager.Status == Shiny.AccessState.Available && !_centralManager.IsScanning)
                {
                    _scanDisposable = _centralManager.ScanForUniquePeripherals().Subscribe(scanResult =>
                    {
                        if (!string.IsNullOrEmpty(scanResult.Name) && !Peripherals.Contains(scanResult))
                            Peripherals.Add(scanResult);

                    });
                }
            }
            IsScanning = _centralManager.IsScanning;
        }
        */
    }
}
