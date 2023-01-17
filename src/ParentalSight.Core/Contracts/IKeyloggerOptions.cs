namespace ParentalSight
{
    public interface IKeyloggerOptions
    {
        int LogRotationInMilliseconds { get; }
        string OutputPath { get; }
    }
}
