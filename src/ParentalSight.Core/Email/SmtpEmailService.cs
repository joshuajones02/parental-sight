namespace ParentalSight.Core.Email
{
    using FluentEmail.Core;
    using FluentEmail.Core.Models;
    using ParentalSight;
    using System;
    using System.Linq;
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;

    internal class SmtpEmailService : IEmailClient
    {
        private readonly IFluentEmailFactory _factory;
        private readonly ISmtpEmailOptions _options;

        public SmtpEmailService(IFluentEmailFactory factory, ISmtpEmailOptions options)
        {
            _factory = factory;
            _options = options;
        }

        public Task SendAsync(string subject, string body, CancellationToken stoppingToken) =>
            _factory.Create()
                    .To(_options.Recipients.Where(x => !string.IsNullOrEmpty(x))
                                           .Select(address => new Address(address)))
                    .Subject(subject)
                    .Body(body)
                    .SendAsync(stoppingToken);
    }
}