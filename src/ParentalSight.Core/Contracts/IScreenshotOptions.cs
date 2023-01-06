namespace ParentalSight.Core
{
    public interface IScreenshotOptions
    {
        long CaptureDelayInMilliseconds { get; }
        string OutputPath { get; }
    }
}