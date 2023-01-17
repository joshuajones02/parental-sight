namespace ParentalSight.Core.Contracts
{
    internal interface IScreenshotClient
    {
        void CaptureScreen(string outputPath, string filename);
    }
}