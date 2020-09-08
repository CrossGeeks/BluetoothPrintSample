using Microsoft.Extensions.DependencyInjection;
using Shiny;

namespace BluetoothPrintSample
{
    public class ShinyAppStartup : Shiny.ShinyStartup
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.UseBleClient();
            services.UseBleHosting();
        }
    }
}
