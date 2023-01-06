namespace ParentalSight
{
    using System.Threading;
    using System.Threading.Tasks;

    public interface IEmailClient
    {
        Task SendAsync(string subject, string body, CancellationToken stoppingToken);
    }
}