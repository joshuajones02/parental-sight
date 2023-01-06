namespace ParentalSight.Core.Screenshot
{
    internal class ScreenshotOptions : IScreenshotOptions
    {
        public ScreenshotOptions(long captureDelayInMilliseconds, string outputPath)
        {
            CaptureDelayInMilliseconds = captureDelayInMilliseconds;
            OutputPath = outputPath;
        }

        public long CaptureDelayInMilliseconds { get; protected set; }

        public string OutputPath { get; protected set; }
    }
}