namespace ParentalSight.Email
{
    using FluentEmail.Core;
    using System.Threading.Tasks;

    internal class EmailService : IEmailClient
    {
        private readonly IFluentEmail _fluentEmail;

        public EmailService(IFluentEmail fluentEmail)
        {
            _fluentEmail = fluentEmail;
        }

        public Task SendAsync(string recipient, string subject, string body) =>
            _fluentEmail.To(recipient)
                        .Subject(subject)
                        .Body(body)
                        .SendAsync();
    }
}