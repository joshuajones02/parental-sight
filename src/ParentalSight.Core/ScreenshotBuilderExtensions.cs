namespace ParentalSight.Core
{
    using Microsoft.Extensions.DependencyInjection;
    using ParentalSight.Core.Screenshot;

    public static class ScreenshotBuilderExtensions
    {
        public static IServiceCollection AddScreenshotService(this IServiceCollection services, long captureDelayInMilliseconds, string outputPath)
        {
            services.AddSingleton<IScreenshotOptions>(
                new ScreenshotOptions(captureDelayInMilliseconds, outputPath));
            services.AddTransient<IScreenshotService, ScreenshotService>();

            return services;
        }
    }
}