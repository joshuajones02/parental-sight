namespace ParentalSight.Core
{
    using Microsoft.Extensions.DependencyInjection;
    using ParentalSight.Core.Contracts;
    using ParentalSight.Core.Screenshot;
    using ParentalSight.Core.Webcam;
    using System;

    public static class WebcamBuilderExtensions
    {
        public static IServiceCollection AddWebcamService(this IServiceCollection services, Action<WebcamOptions> configure)
        {
            var options = new WebcamOptions();
            configure(options);

            return services.AddWebcamService(options);
        }

        public static IServiceCollection AddWebcamService(this IServiceCollection services, WebcamOptions options)
        {
            services.AddSingleton<IWebcamOptions>(options);
            services.AddTransient<IWebcamClient, WebcamClient>();
            services.AddTransient<IWebcamService, WebcamService>();

            return services;
        }
    }
}