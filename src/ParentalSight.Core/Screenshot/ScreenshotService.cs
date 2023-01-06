namespace ParentalSight.Core.Screenshot
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    public class FilenameBuilder
    {
        private readonly DateTime _dateTime;

        public FilenameBuilder(DateTime dateTime)
        {
            _dateTime = dateTime;
        }

        private string _prefix;
        public FilenameBuilder WithPrefix(string prefix)
        {
            _prefix = prefix;

            return this;
        }

        public string Build() =>
            $"{_prefix}{_dateTime}";
    }

    internal class ScreenshotService : IScreenshotService
    {
        protected string GenerateFilename()
        {
            var filename = DateTime.ToString("yyyyMMdd-HHmm");
        }

        public async Task StartAsync(long captureDelayInMilliseconds, string outputPath, CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {

                CaptureScreen(outputPath, filename);
                await Task.Delay((int)captureDelayInMilliseconds);
            }
        }

        protected void CaptureScreen(string outputPath, string filename)
        {
            var width = SystemInformation.VirtualScreen.Width;
            var height = SystemInformation.VirtualScreen.Height;

            using (var bitmap = new Bitmap(width, height))
            {
                using (var graphic = Graphics.FromImage(bitmap))
                {
                    graphic.CopyFromScreen(0, 0, 0, 0, bitmap.Size);
                }

                var path = Path.Combine(outputPath, $"{filename}.png");

                bitmap.Save(path, ImageFormat.Png);
            }
        }
    }
}