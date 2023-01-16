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
        private readonly string _dateTime;

        public FilenameBuilder() : this(DateTime.Now)
        {
        }

        public FilenameBuilder(DateTime dateTime)
        {
            _dateTime = dateTime.ToString("yyyyMMdd-HHmm");
        }

        private string _prefix;
        public FilenameBuilder WithPrefix(string prefix)
        {
            _prefix = prefix;

            return this;
        }

        private string _fileExtension;
        public FilenameBuilder WithExtension(string fileExtension)
        {
            _fileExtension = fileExtension.StartsWith('.') ?
                fileExtension : string.Concat('.', fileExtension);

            return this;
        }

        public string Build() =>
            $"{_prefix}{_dateTime}{_fileExtension}";
    }

    internal class ScreenshotService : IScreenshotService
    {
        public async Task StartAsync(long captureDelayInMilliseconds, string outputPath, CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var filename = new FilenameBuilder().WithPrefix("screenshot-").WithExtension("PNG").Build();
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