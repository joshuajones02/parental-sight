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
            var output = Path.Combine(_options.OutputPath, Environment.UserName);
            if (!Directory.Exists(output))
            {
                Directory.CreateDirectory(output);
            }

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                var filename = new FilenameBuilder().WithPrefix("screenshot-").Build();
                _client.CaptureScreen(output, filename);
                await Task.Delay(_options.CaptureDelayInMilliseconds);
            }
        }
    }
}