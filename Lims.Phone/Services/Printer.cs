using Shiny;
using Shiny.BluetoothLE;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace Lims.Phone.Services
{
    public static class BlueToothPrinter
    {
        private static IDisposable _scanDisposable;
        private static IDisposable _connectedDisposable;
        private static IBleManager _centralManager = Shiny.ShinyHost.Resolve<IBleManager>();

        public static bool IsScanning { get; set; }
        public static ObservableCollection<IPeripheral> Peripherals { get; set; } = new ObservableCollection<IPeripheral>();

        /// <summary>
        /// 获取蓝牙设备清单
        /// </summary>
        public static void GetDeviceList()
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

        public static async Task SetAdapter()
        {
            var poweredOn = _centralManager.Status == AccessState.Available;
            if (!poweredOn && !_centralManager.TrySetAdapterState(true))
                await Xamarin.Forms.Application.Current.MainPage.DisplayAlert("提醒", "无法更改蓝牙适配器状态", "确定");

            if (poweredOn)
            {
                GetConnectedDevices();
            }
        }

        public static void GetConnectedDevices()
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

        public static async Task CheckPermissions()
        {
            var status = await _centralManager.RequestAccess();
            if (status == AccessState.Denied)
            {
                await App.Current.MainPage.DisplayAlert("权限", "你需要打开你的蓝牙才能使用这个应用程序。", "确定");
                Xamarin.Essentials.AppInfo.ShowSettingsUI();
            }
            else
            {
                await SetAdapter();
            }
        }
    }
}
