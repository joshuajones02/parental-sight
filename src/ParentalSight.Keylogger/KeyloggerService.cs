namespace ParentalSight.BackgroundService.Keylogger
{
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public class KeyloggerService
    {
        private readonly ILogger<KeyloggerService> _logger;

        public KeyloggerService(ILogger<KeyloggerService> logger)
        {
            _logger = logger;
        }

        protected async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
