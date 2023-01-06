namespace ParentalSight
{
    public interface IKeyloggerOptions
    {
        long LogRotationInMilliseconds { get; }
        string OutputPath { get; }
    }
}
