namespace ParentalSight.Core
{
    using Microsoft.Extensions.DependencyInjection;
    using ParentalSight.Core.Keylogger;
    using System;

    public static class KeyloggerBuilderExtensions
    {
        public static IServiceCollection AddKeylogger(this IServiceCollection services, IKeyloggerOptions options)
        {
            if (options.LogRotationInMilliseconds is default(int))
                throw new ArgumentException(nameof(options.LogRotationInMilliseconds));
            if (string.IsNullOrEmpty(options.OutputPath))
                throw new ArgumentException(nameof(options.OutputPath));

            services.AddSingleton<IKeyloggerOptions>(options);
            services.AddTransient<IKeyloggerService, KeyloggerService>();

            return services;
        }
    }
}