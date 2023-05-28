namespace ParentalSight.WindowsService
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using ParentalSight.Core;
    using ParentalSight.WindowsService.Settings;
    using Serilog;
    using System.IO;
    using System.Linq;

    internal static class Startup
    {
        public static void ConfigureConfiguration(this IConfigurationBuilder builder, HostBuilderContext context)
        {
            builder.Sources.Clear();
            builder.AddJsonFile("appsettings.json", optional: false);
            builder.AddJsonFile("appsettings.secrets.json", optional: false);
            builder.AddEnvironmentVariables();
#if DEBUG
            builder.AddJsonFile("appsettings.debug.json", optional: false);
#endif
            context.Configuration = builder.Build();
        }

        public static void ConfigureLogging(this ILoggingBuilder builder, HostBuilderContext context)
        {
            var serilogTemplate = "{Timestamp:HH:mm:ss.fff} [{Level:u3}] {Message:lj}{NewLine}{Exception}";
            var settings = new MiscSettings(context.Configuration);
            var loggerConfiguration = new LoggerConfiguration()
                .MinimumLevel.Debug();
            loggerConfiguration.WriteTo.Async(x => x.File(
                                    path: Path.Combine(settings.OutputPath, "system.log"),
                restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Debug,
                          outputTemplate: serilogTemplate,
                         rollingInterval: RollingInterval.Day,
                                buffered: false,
                                  shared: false));
#if DEBUG
            loggerConfiguration.WriteTo.Async(x => x.Debug(outputTemplate: serilogTemplate));
            loggerConfiguration.WriteTo.Async(x => x.Console(outputTemplate: serilogTemplate));
#endif
            Log.Logger = loggerConfiguration.CreateLogger();
            // See: https://github.com/dotnet/runtime/issues/47303
            var loggingSettings = context.Configuration.GetSection("Logging");
            builder.AddConfiguration(loggingSettings);
        }

        public static IServiceCollection ConfigureServices(this IServiceCollection services, HostBuilderContext context, string[] args)
        {
            var config = context.Configuration;
            services.AddHostedServices(config, args);
            // TODO: Move or implement somewhere better
            services.AddSmtpEmailService(
                   port: config.GetRequiredValue<int>("email:smtp:port"),
                   host: config.GetRequiredValue<string>("email:smtp:host"),
                 sender: config.GetRequiredValue<string>("email:sender"),
               password: config.GetRequiredValue<string>("email:smtp:password"),
               username: config.GetRequiredValue<string>("email:smtp:username"),
             recipients: config.GetSection("email:recipients")?
                               .AsEnumerable()?
                               .Select(x => x.Value)?
                               .ToArray());

            return services;
        }
    }
}