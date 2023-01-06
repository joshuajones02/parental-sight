namespace ParentalSight
{
    public interface ISmtpEmailOptions
    {
        int Port { get; }
        string Host { get; }
        string Password { get; }
        string Sender { get; }
        string Username { get; }
        string[] Recipients { get; }
    }
}