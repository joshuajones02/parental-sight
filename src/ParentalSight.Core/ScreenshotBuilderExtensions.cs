namespace ParentalSight.Core
{
    using Microsoft.Extensions.DependencyInjection;
    using ParentalSight.Core.Contracts;
    using ParentalSight.Core.Screenshot;
    using System;

    public static class ScreenshotBuilderExtensions
    {
        public static IServiceCollection AddScreenshotService(this IServiceCollection services, Action<ScreenshotOptions> configure)
        {
            var options = new ScreenshotOptions();
            configure(options);

            return services.AddScreenshotService(options);
        }

        public static IServiceCollection AddScreenshotService(this IServiceCollection services, ScreenshotOptions options)
        {
            services.AddSingleton<IScreenshotOptions>(options);
            services.AddTransient<IScreenshotClient, ScreenshotClient>();
            services.AddTransient<IScreenshotService, ScreenshotService>();

            return services;
        }
    }
}