namespace ParentalSight.Core.Screenshot
{
    using Microsoft.Extensions.Logging;
    using ParentalSight.Core.Contracts;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Windows.Forms;

    internal class ScreenshotClient : IScreenshotClient
    {
        private readonly ILogger<ScreenshotClient> _logger;

        public ScreenshotClient(ILogger<ScreenshotClient> logger)
        {
            _logger = logger;
        }

        public void CaptureScreen(string outputPath, string filename)
        {
            var width = SystemInformation.VirtualScreen.Width;
            var height = SystemInformation.VirtualScreen.Height;

            using (var bitmap = new Bitmap(width, height))
            {
                using (var graphic = Graphics.FromImage(bitmap))
                {
                    _logger.LogInformation("Starting Copy from Screen");
                    graphic.CopyFromScreen(0, 0, 0, 0, bitmap.Size);
                    _logger.LogInformation("Completed Copy from Screen");
                }

                // TODO: Allow filetype to be option configurable
                var path = Path.Combine(outputPath, $"{filename}.png");
                _logger.LogInformation($"OutputPath : {path}");

                bitmap.Save(path, ImageFormat.Png);
            }
        }
    }
}