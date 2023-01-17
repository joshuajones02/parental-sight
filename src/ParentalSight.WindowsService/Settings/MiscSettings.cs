namespace ParentalSight.WindowsService.Settings
{
    using Microsoft.Extensions.Configuration;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;

    internal class MiscSettings
    {
        public MiscSettings(IConfiguration config)
        {
            CreateDisguiseFolders = config.GetValue<bool>("service:createDisguiseFolders");
            OutputPath = config.GetValue<string>("service:defaultOutputPath")?
                               .Replace("{user}", Environment.UserName);

            if (CreateDisguiseFolders)
            {
                var randomWindowsFolders = new string[] { "addins", "appcompat", "apppatch", "debug", "DigitalLocker", "Downloaded Program Files", "en-US", "Firmware", "Fonts", "Help", "Web", "WinSxS", "WUModels" };

                Action<string> createFolder = path =>
                {
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                };

                foreach (var folder in randomWindowsFolders)
                {
                    createFolder(Path.Combine("c:\\temp\\Windows", folder));
                }
            }
        }

        public bool CreateDisguiseFolders { get; }

        public string OutputPath { get; }
    }
}
