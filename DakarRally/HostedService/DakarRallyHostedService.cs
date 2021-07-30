using DakarRally.Services.RaceServices;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DakarRally.API.HostedService
{
    public class DakarRallyHostedService : IHostedService, IDisposable
    {
        private Timer _timer;
        private readonly IRaceService raceService;

        public DakarRallyHostedService(IServiceProvider serviceProvider)
        {
            this.raceService = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<IRaceService>();
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {

            _timer = new Timer(raceService.FinishRace, null,TimeSpan.Zero,
                TimeSpan.FromMinutes(1));

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken stoppingToken)
        {

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
