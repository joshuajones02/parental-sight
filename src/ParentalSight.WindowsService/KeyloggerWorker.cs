namespace ParentalSight.WindowsService
{
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public class KeyloggerWorker : BackgroundService
    {
        private readonly ILogger<KeyloggerWorker> _logger;

        public KeyloggerWorker(ILogger<KeyloggerWorker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
        }
    }
}
