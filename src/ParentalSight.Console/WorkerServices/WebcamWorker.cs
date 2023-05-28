namespace ParentalSight.WindowsService.WorkerServices
{
    using Microsoft.Extensions.Hosting;
    using System.Threading;
    using System.Threading.Tasks;

    public class WebcamWorker : BackgroundService
    {
        private readonly IWebcamService _service;

        public WebcamWorker(IWebcamService service)
        {
            _service = service;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken) =>
            await _service.StartAsync(stoppingToken);
    }
}