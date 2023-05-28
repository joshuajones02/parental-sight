namespace ParentalSight.WindowsService.Settings
{
    using Microsoft.Extensions.Configuration;
    using ParentalSight;
    using System;
    using System.IO;

    internal class KeyloggerSettings : IKeyloggerOptions
    {
        public KeyloggerSettings(IConfiguration config)
        {
            LogRotationInMilliseconds = config.GetValue<int>("service:keylogger:logRotationInMilliseconds");
            OutputPath = config.GetValue<string>("service:defaultOutputPath")?
                               .Replace("{user}", Environment.UserName)
                               .TrimEnd('$'); ;
        }

        public int LogRotationInMilliseconds { get; set; }

        public string OutputPath { get; set; }
    }
}