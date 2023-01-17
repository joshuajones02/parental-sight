namespace ParentalSight.Core.Screenshot
{
    using ParentalSight.Core.Contracts;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Windows.Forms;

    internal class ScreenshotClient : IScreenshotClient
    {
        public void CaptureScreen(string outputPath, string filename)
        {
            var width = SystemInformation.VirtualScreen.Width;
            var height = SystemInformation.VirtualScreen.Height;

            using (var bitmap = new Bitmap(width, height))
            {
                using (var graphic = Graphics.FromImage(bitmap))
                {
                    graphic.CopyFromScreen(0, 0, 0, 0, bitmap.Size);
                }

                // TODO: Allow filetype to be option configurable
                var path = Path.Combine(outputPath, $"{filename}.png");

                bitmap.Save(path, ImageFormat.Png);
            }
        }
    }
}