namespace ParentalSight.Email.Sample
{
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using ParentalSight;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public class SampleSmtpEmailService : BackgroundService
    {
        private readonly IEmailClient _email;
        private readonly ILogger<SampleSmtpEmailService> _logger;

        public SampleSmtpEmailService(IEmailClient email, ILogger<SampleSmtpEmailService> logger)
        {
            _email = email;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                await _email.SendAsync(
                          subject: "THIS IS A TEST",
                             body: string.Concat("Worker running at: ", DateTimeOffset.Now),
                    stoppingToken: stoppingToken);

                await Task.Delay(5000, stoppingToken);
            }
        }
    }
}