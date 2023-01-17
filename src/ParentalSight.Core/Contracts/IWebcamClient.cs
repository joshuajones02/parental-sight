namespace ParentalSight.Core.Contracts
{
    internal interface IWebcamClient
    {
        void Capture(string outputPath, string filename);
    }
}