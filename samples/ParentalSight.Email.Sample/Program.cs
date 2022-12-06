namespace ParentalSight.Email.Sample
{
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
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
                .ConfigureServices((_, services) =>
                    services.AddHostedService<Worker>()
                            .AddSmtpEmailService("smtp-mail.outlook.com", 587, "joshuaandshannonforever@outlook.com", "REPLACE_WITH_SECRET_HERE"))
                .Build();
    }
}