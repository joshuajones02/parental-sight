namespace ParentalSight.WindowsService
{
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using ParentalSight.WindowsService.WorkerServices;
    using Serilog;
    using System;
    using System.Threading.Tasks;

    public class Program
    {
        public static async Task Main(string[] args)
        {
            var _logger = Log.Logger;
            try
            {
                _logger.Information("Starting Windows Service {time}", DateTime.Now);
                using (var host = HostBuilder(args).Build())
                {
                    await host.RunAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.Error("KeyloggerService : {type} {message}\n{stackTrace}", ex.GetType().Name, ex.Message, ex.StackTrace);
                if (ex.InnerException != null)
                {
                    _logger.Error("KeyloggerService : {type} {message}\n{stackTrace}",
                        ex.InnerException.GetType().Name, ex.InnerException.Message, ex.InnerException.StackTrace);
                    throw ex.InnerException;
                }

                throw;
            }
            finally
            {
                _logger.Information("Stopping Windows Service {time}", DateTime.Now);
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
                .UseWindowsService(options => options.ServiceName = "ps-winsvc")
#endif
                .UseSerilog();
    }
}