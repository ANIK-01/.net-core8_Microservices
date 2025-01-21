
using Serilog;
using Serilog.Formatting.Json;

namespace CatalogApi
{
    public static class Configuration
    {
        public static void SerilogConfiguration(this IHostBuilder host)
        {
            host.UseSerilog(configureLogger:(context, loggerConfig) =>

            {
                loggerConfig.WriteTo.Console();
                loggerConfig.WriteTo.File(new JsonFormatter(),path: "logs/catalogs.txt", rollingInterval:RollingInterval.Day);


            });
        }
    }
}
