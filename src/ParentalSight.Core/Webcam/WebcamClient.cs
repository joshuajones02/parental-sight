namespace ParentalSight.Core.Screenshot
{
    using Emgu.CV;
    using ParentalSight.Core.Contracts;
    using System.IO;

    internal class WebcamClient : IWebcamClient
    {
        public void Capture(string outputPath, string filename)
        {
            using (var capture = new VideoCapture())
            {
                using (var image = capture.QueryFrame())
                {
                    var filepath = Path.Combine(outputPath, $"{filename}.png");
                    image.Save(filepath);
                }
            }
        }
    }
}