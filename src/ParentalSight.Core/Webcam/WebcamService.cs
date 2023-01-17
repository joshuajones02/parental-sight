namespace ParentalSight.Core.Screenshot
{
    using ParentalSight.Common.Builders;
    using ParentalSight.Core.Contracts;
    using System;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;

    internal class WebcamService : IWebcamService
    {
        private readonly IWebcamClient _client;
        private readonly IWebcamOptions _options;

        public WebcamService(IWebcamClient client, IWebcamOptions options)
        {
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
                var filename = new FilenameBuilder().WithPrefix("webcam-").Build();
                _client.Capture(output, filename);
                await Task.Delay(_options.CaptureDelayInMilliseconds);
            }
        }
    }
}