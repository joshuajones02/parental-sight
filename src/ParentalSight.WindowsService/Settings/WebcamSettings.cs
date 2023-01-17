namespace ParentalSight.WindowsService.Settings
{
    using ParentalSight.Core;
    using Microsoft.Extensions.Configuration;
    using System.IO;
    using ParentalSight.Core.Webcam;
    using System;

    internal class WebcamSettings : WebcamOptions, IScreenshotOptions
    {
        public WebcamSettings(IConfiguration config)
        {
            CaptureDelayInMilliseconds = config.GetValue<int>("service:screenshot:captureDelayInMilliseconds");
            OutputPath = config.GetValue<string>("service:defaultOutputPath")?
                               .Replace("{user}", Environment.UserName);
        }

        public override int CaptureDelayInMilliseconds { get; set; }

        public override string OutputPath { get; set; }
    }
}