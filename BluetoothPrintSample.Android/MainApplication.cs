using System;
using Android.App;
using Android.Runtime;
using Shiny;

namespace BluetoothPrintSample.Droid
{
#if DEBUG
    [Application(Debuggable = true)]
#else
        [Application(Debuggable = false)]
#endif
    public class MainApplication : ShinyAndroidApplication<ShinyAppStartup>
    {
        public MainApplication(IntPtr handle, JniHandleOwnership transfer) : base(handle, transfer)
        {
        }
    }
}