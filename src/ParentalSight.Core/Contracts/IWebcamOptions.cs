namespace ParentalSight.Core
{
    public interface IWebcamOptions
    {
        int CaptureDelayInMilliseconds { get; }
        string OutputPath { get; }
    }
}