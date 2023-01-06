﻿namespace ParentalSight
{
    using System.Threading;
    using System.Threading.Tasks;

    public interface IScreenshotService
    {
        Task StartAsync(long captureDelayInMilliseconds, string outputPath, CancellationToken stoppingToken);
    }
}