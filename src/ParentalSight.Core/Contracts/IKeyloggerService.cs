namespace ParentalSight
{
    using System.Threading.Tasks;

    public interface IKeyloggerService
    {
        Task StartAsync(long expirationInMilliseconds, string outputPath);
    }
}
