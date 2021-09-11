using Infrastructure.Interfaces.Logger;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UseCases.Flats.BackgroundJobs;

namespace WepApp.HostedServices
{
    public class ParseCianJob : BackgroundService
    {
        private Timer _timer;
        private ILoggerService _loggerService;
        private IServiceScopeFactory _factory;

        public ParseCianJob(
            ILoggerService loggerService,
            IServiceScopeFactory serviceScopeFactory)
        {
            _loggerService = loggerService;
            _factory = serviceScopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _loggerService.Info("Start MyJob");

            _timer = new Timer(Execute, null, TimeSpan.Zero, TimeSpan.FromHours(8));
        }

        private async void Execute(object state)
        {
            _loggerService.Info("Start job");
            using (var scope = _factory.CreateScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<ParseCianRentFlatJob>();
                service.Execute().Wait();
            }
        }
    }
}
