namespace ParentalSight.Core.Screenshot
{
    public class ScreenshotOptions : IScreenshotOptions
    {
        public ScreenshotOptions()
        {
        }

        public ScreenshotOptions(int captureDelayInMilliseconds, string outputPath)
        {
            CaptureDelayInMilliseconds = 10_000; // captureDelayInMilliseconds;
            OutputPath = outputPath;
        }

        public virtual int CaptureDelayInMilliseconds { get; set; }

        public virtual string OutputPath { get; set; }
    }
}