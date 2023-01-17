namespace ParentalSight
{
    using System.Threading;
    using System.Threading.Tasks;

    public interface IKeyloggerService
    {
        Task StartAsync(CancellationToken stoppingToken);
    }
}
