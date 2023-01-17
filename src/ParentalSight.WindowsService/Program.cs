namespace ParentalSight.WindowsService
{
    using Microsoft.Extensions.Hosting;
    using Serilog;
    using System;
    using System.Threading.Tasks;

    public class Program
    {
        public static async Task Main(string[] args)
        {
            try
            {
                using (var host = HostBuilder(args).Build())
                {
                    await host.RunAsync();
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    throw ex.InnerException;
                throw;
            }
        }

        public static IHostBuilder HostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, builder) => builder.ConfigureConfiguration(context))
                .ConfigureLogging((context, builder) => builder.ConfigureLogging(context))
#if DEBUG
                .ConfigureServices((context, services) =>
                {
                    services.ConfigureServices(context);
                    var worker = Environment.GetEnvironmentVariable("DEBUG_WORKER");
                    services.AddHostedService(context.Configuration, worker);
                })
#else
                .ConfigureServices((context, services) =>
                    services.ConfigureServices(context)
                            .AddHostedServices(context.Configuration))
                .UseWindowsService(options => options.ServiceName = "ps-winsvc");
#endif
                .UseSerilog();
    }
}