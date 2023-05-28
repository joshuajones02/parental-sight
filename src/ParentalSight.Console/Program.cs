namespace ParentalSight.WindowsService
{
    using Microsoft.Extensions.Hosting;
    using Serilog;
    using System;

    public class Program
    {
        public static void Main(string[] args)
        {
            var _logger = Log.Logger;

            try
            {
                _logger.Information("Starting Hosted Service {time}", DateTime.Now);
                using (var host = HostBuilder(args).Build())
                {
                    host.Run();
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
                _logger.Information("Stopping Hosted Service {time}", DateTime.Now);
            }
        }

        public static IHostBuilder HostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, builder) => builder.ConfigureConfiguration(context))
                .ConfigureLogging((context, builder) => builder.ConfigureLogging(context))
                .ConfigureServices((context, services) => services.ConfigureServices(context, new[] { "webcam" }))
                .UseSerilog();
    }
}