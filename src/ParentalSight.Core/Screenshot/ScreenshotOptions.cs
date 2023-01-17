namespace ParentalSight.Core.Screenshot
{
    public class ScreenshotOptions : IScreenshotOptions
    {
        public ScreenshotOptions()
        {
        }

        public ScreenshotOptions(int captureDelayInMilliseconds, string outputPath)
        {
            CaptureDelayInMilliseconds = captureDelayInMilliseconds;
            OutputPath = outputPath;
        }

        public virtual int CaptureDelayInMilliseconds { get; set; }

        public virtual string OutputPath { get; set; }
    }
}