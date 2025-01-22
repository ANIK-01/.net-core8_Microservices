
using Serilog;
using Serilog.Formatting.Json;

namespace CatalogApi
{
    public static class Configuration
    {
        public static void SerilogConfiguration(this IHostBuilder host)
        {
            host.UseSerilog((context, loggerConfig) =>

            {
                loggerConfig.ReadFrom.Configuration(context.Configuration);

            });
        }
    }
}
