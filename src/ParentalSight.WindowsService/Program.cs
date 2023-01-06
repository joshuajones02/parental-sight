namespace ParentalSight.WindowsService
{
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using System;
    using System.Threading.Tasks;

    public class Program
    {
        public static async Task Main(string[] args)
        {
            try
            {
                using (var host = CreateHostBuilder(args))
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

        public static IHost CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureHostConfiguration(builder => builder.BuildServiceConfiguration())
                .ConfigureLogging((context, builder) => builder.ConfigureLogging(context))
                .ConfigureServices((context, services) => services.ConfigureServices(context))
                .UseWindowsService(options => options.ServiceName = "ps-winsvc")
                .Build();
    }
}