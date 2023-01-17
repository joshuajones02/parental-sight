namespace ParentalSight.Core
{
    using Microsoft.Extensions.DependencyInjection;
    using ParentalSight;
    using ParentalSight.Core.Email;

    public static class EmailBuilderExtensions
    {
        public static IServiceCollection AddSmtpEmailService(this IServiceCollection services, string host, int port, string sender, string password, params string[] recipients) =>
            services.AddSmtpEmailService(host, port, sender, sender, password, recipients);

        public static IServiceCollection AddSmtpEmailService(this IServiceCollection services, string host, int port, string sender, string username, string password, params string[] recipients)
        {
            services.AddSingleton<ISmtpEmailOptions>(new SmtpEmailOptions(host, port, sender, username, password, recipients));
            services.AddFluentEmail(sender)
                    .AddSmtpSender(host, port, username, password);

            services.AddTransient<IEmailClient, SmtpEmailService>();

            return services;
        }
    }
}