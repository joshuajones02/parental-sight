namespace ParentalSight.Email.Sample
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using ParentalSight.Core;
    using ParentalSight.Core.Email;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    public class Program
    {
        public static async Task Main(string[] args)
        {
            using (var host = CreateHostBuilder(args))
            {
                await host.RunAsync();
            }
        }

        public static IHost CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(builder =>
                {
                    builder.Sources.Clear();
                    builder.AddJsonFile("appsettings.json", optional: false);
                    builder.AddJsonFile("appsettings.secrets.json", optional: false);
                })
                .ConfigureServices((context, services) =>
                {
                    var config = context.Configuration;
                    services.AddHostedService<SampleSmtpEmailService>()
                            .AddSmtpEmailService(
                               port: config.GetValue<int>("email:smtp:port"),
                               host: config.GetValue<string>("email:smtp:host"),
                             sender: config.GetValue<string>("email:sender"),
                           password: config.GetValue<string>("email:smtp:password"),
                           username: config.GetValue<string>("email:smtp:username"),
                         recipients: config.GetSection("email:recipients")?
                                           .AsEnumerable()?
                                           .Select(x => x.Value)?
                                           .ToArray());
                })
                .Build();
    }
}