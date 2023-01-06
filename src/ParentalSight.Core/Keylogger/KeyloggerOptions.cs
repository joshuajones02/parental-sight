namespace ParentalSight.Core.Keylogger
{
    using ParentalSight;

    internal class KeyloggerOptions : IKeyloggerOptions
    {
        public KeyloggerOptions(long logRotationInMilliseconds, string outputPath)
        {
            LogRotationInMilliseconds = logRotationInMilliseconds;
            OutputPath = outputPath;
        }

        public long LogRotationInMilliseconds { get; protected set; }

        public string OutputPath { get; protected set; }
    }
}
