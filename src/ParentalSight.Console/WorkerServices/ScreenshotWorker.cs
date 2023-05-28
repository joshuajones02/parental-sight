namespace ParentalSight.WindowsService.WorkerServices
{
    using Microsoft.Extensions.Hosting;
    using System.Threading;
    using System.Threading.Tasks;

    public class ScreenshotWorker : BackgroundService
    {
        private readonly IScreenshotService _service;

        public ScreenshotWorker(IScreenshotService service)
        {
            _service = service;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken) =>
            await _service.StartAsync(stoppingToken);
    }
}