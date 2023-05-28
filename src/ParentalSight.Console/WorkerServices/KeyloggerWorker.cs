namespace ParentalSight.WindowsService.WorkerServices
{
    using Microsoft.Extensions.Hosting;
    using System.Threading;
    using System.Threading.Tasks;

    public class KeyloggerWorker : BackgroundService
    {
        private readonly IKeyloggerService _service;

        public KeyloggerWorker(IKeyloggerService service)
        {
            _service = service;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken) =>
            await _service.StartAsync(stoppingToken);
    }
}