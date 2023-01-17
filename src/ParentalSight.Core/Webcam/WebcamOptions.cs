namespace ParentalSight.Core.Webcam
{
    public class WebcamOptions : IWebcamOptions
    {
        public WebcamOptions()
        {
        }

        public virtual int CaptureDelayInMilliseconds { get; set; }

        public virtual string OutputPath { get; set; }
    }
}