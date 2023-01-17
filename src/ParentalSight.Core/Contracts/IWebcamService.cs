namespace ParentalSight
{
    using System.Threading;
    using System.Threading.Tasks;

    public interface IWebcamService
    {
        Task StartAsync(CancellationToken stoppingToken);
    }
}