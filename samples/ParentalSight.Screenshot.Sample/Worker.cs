namespace ParentalSight.Screenshot.Sample
{
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using System;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;

    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IScreenshotService _screenshot;

        public Worker(ILogger<Worker> logger, IScreenshotService screenshot)
        {
            _logger = logger;
            _screenshot = screenshot;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                //_screenshot.Capture(AppDomain.CurrentDomain.BaseDirectory, string.Empty);

                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}