namespace ParentalSight.Core.Screenshot
{
    using Microsoft.Extensions.Logging;
    using ParentalSight.Common.Builders;
    using ParentalSight.Core.Contracts;
    using System;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;

    internal class WebcamService : IWebcamService
    {
        private readonly ILogger<WebcamService> _logger;
        private readonly IWebcamClient _client;
        private readonly IWebcamOptions _options;

        public WebcamService(ILogger<WebcamService> logger, IWebcamClient client, IWebcamOptions options)
        {
            _logger = logger;
            _client = client;
            _options = options;
        }

        public async Task StartAsync(CancellationToken stoppingToken)
        {
            try
            {
                // TODO: Move to central point which also includes ther logged in users name
                if (!Directory.Exists(_options.OutputPath))
                {
                    Directory.CreateDirectory(_options.OutputPath);
                }

                while (!stoppingToken.IsCancellationRequested)
                {
                    _logger.LogInformation("WebcamService : Output path: {path}", _options.OutputPath);
                    _logger.LogInformation("WebcamService : Worker running at: {time}", DateTimeOffset.Now);
                    var filename = new FilenameBuilder().WithPrefix("webcam-").Build();
                    _logger.LogInformation("WebcamService : Filename: {filename}", filename);
                    _client.Capture(_options.OutputPath, filename);
                    await Task.Delay(_options.CaptureDelayInMilliseconds);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("WebcamService : {type} {message}\n{stackTrace}", ex.GetType().Name, ex.Message, ex.StackTrace);
                if (ex.InnerException != null)
                {
                    _logger.LogError("WebcamService : {type} {message}\n{stackTrace}",
                        ex.InnerException.GetType().Name, ex.InnerException.Message, ex.InnerException.StackTrace);
                }
            }
        }
    }
}