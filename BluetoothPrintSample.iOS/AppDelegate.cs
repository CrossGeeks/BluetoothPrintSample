using Foundation;
using UIKit;

namespace BluetoothPrintSample.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();

            Shiny.iOSShinyHost.Init(new ShinyAppStartup());
            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }
    }
}
