namespace ParentalSight.Core
{
    public interface IScreenshotOptions
    {
        int CaptureDelayInMilliseconds { get; }
        string OutputPath { get; }
    }
}