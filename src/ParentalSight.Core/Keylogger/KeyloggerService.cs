namespace ParentalSight.Core.Keylogger
{
    using ParentalSight;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    internal class KeyloggerService : IKeyloggerService
    {
        public Task StartAsync(long logRotationInMilliseconds, string outputPath)
        {
            throw new NotImplementedException();
        }
    }
}
