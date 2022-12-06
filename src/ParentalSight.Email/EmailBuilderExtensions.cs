namespace ParentalSight.Email
{
    using Microsoft.Extensions.DependencyInjection;

    public static class EmailBuilderExtensions
    {
        public static IServiceCollection AddSmtpEmailService(this IServiceCollection services, string host, int port, string emailAddress, string password) =>
            services.AddSmtpEmailService(host, port, emailAddress, emailAddress, password);

        public static IServiceCollection AddSmtpEmailService(this IServiceCollection services, string host, int port, string emailAddress, string username, string password)
        {
            services.AddFluentEmail(emailAddress)
                    .AddSmtpSender(host, port, username, password);

            services.AddTransient<IEmailClient, EmailService>();

            return services;
        }
    }
}