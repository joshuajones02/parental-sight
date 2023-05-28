namespace ParentalSight.Core.Screenshot
{
    using Microsoft.Extensions.Logging;
    using ParentalSight.Common.Builders;
    using ParentalSight.Core.Contracts;
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    internal class ScreenshotService : IScreenshotService
    {
        private readonly ILogger<ScreenshotService> _logger;
        private readonly IScreenshotClient _client;
        private readonly IScreenshotOptions _options;

        public ScreenshotService(ILogger<ScreenshotService> logger, IScreenshotClient client, IScreenshotOptions options)
        {
            _logger = logger;
            _client = client;
            _options = options;
        }

        public async Task StartAsync(CancellationToken stoppingToken)
        {
            // TODO: Move to central point which also includes ther logged in users name
            var output = Path.Combine(_options.OutputPath);
            if (!Directory.Exists(output))
            {
                Directory.CreateDirectory(output);
            }

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    _logger.LogInformation("ScreenshotService : Output path: {path}", output);
                    _logger.LogInformation("ScreenshotService : Worker running at: {time}", DateTimeOffset.Now);
                    var filename = new FilenameBuilder().WithPrefix("screenshot-").Build();
                    _logger.LogInformation("ScreenshotService : Filename: {filename}", filename);
                    _client.CaptureScreen(output, filename);
                    await Task.Delay(_options.CaptureDelayInMilliseconds);
                }
                catch (Exception ex)
                {
                    _logger.LogError("ScreenshotService : {type} {message}\n{stackTrace}", ex.GetType().Name, ex.Message, ex.StackTrace);
                    if (ex.InnerException != null)
                    {
                        _logger.LogError("ScreenshotService : {type} {message}\n{stackTrace}",
                            ex.InnerException.GetType().Name, ex.InnerException.Message, ex.InnerException.StackTrace);
                    }
                }
            }
        }
    }
}