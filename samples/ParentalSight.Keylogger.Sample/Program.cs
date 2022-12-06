namespace ParentalSight.Keylogger.Sample
{
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using System.Threading.Tasks;

    internal class Program
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
                .ConfigureServices((_, services) => services.AddHostedService<Worker>())
                .Build();
    }
}
