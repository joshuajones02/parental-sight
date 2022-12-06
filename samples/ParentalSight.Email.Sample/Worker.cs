namespace ParentalSight.Email.Sample
{
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public class Worker : BackgroundService
    {
        private readonly IEmailClient _email;
        private readonly ILogger<Worker> _logger;

        public Worker(IEmailClient email, ILogger<Worker> logger)
        {
            _email = email;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await _email.SendAsync("joshuajones0222@gmail.com", "Test", $"Worker running at: {DateTimeOffset.Now}");

                await Task.Delay(5000, stoppingToken);
            }
        }
    }
}
