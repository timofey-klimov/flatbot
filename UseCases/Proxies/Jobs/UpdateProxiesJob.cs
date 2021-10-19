using Infrastructure.Interfaces.DataAccess;
using Infrastructure.Interfaces.Jobs;
using Infrastructure.Interfaces.Logger;
using Infrastructure.Interfaces.Proxy.HttpClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UseCases.Proxies.Jobs
{
    public class UpdateProxiesJob : IJob
    {
        private readonly IProxyHttpClient _proxyHttpClient;
        private readonly ILoggerService _logger;
        private readonly IDbContext _dbContext;
        private readonly IServiceScopeFactory _scopeFactory;

        public UpdateProxiesJob(
            IProxyHttpClient proxyHttpClient, 
            ILoggerService logger,
            IDbContext dbContext,
            IServiceScopeFactory serviceScopeFactory)
        {
            _proxyHttpClient = proxyHttpClient;
            _logger = logger;
            _dbContext = dbContext;
            _scopeFactory = serviceScopeFactory;
        }

        public async Task ExecuteAsync(CancellationToken token = default)
        {
            var proxies = await _dbContext.Proxies
                .AsNoTracking()
                .ToArrayAsync();

            var tasks = new List<Task>();

            foreach (var proxy in proxies)
            {
                var task = Task.Run(async () =>
                {
                    try
                    {
                        using var scope = _scopeFactory.CreateScope();
                        var dbContext = scope.ServiceProvider.GetRequiredService<IDbContext>();

                        _logger.Info(this.GetType(), $"start check old proxy {proxy.Ip} {proxy.Port}");

                        var result = await _proxyHttpClient.CheckProxyAsync(proxy.Ip, proxy.Port);

                        if (!result)
                        {
                            var proxyToDelete = await dbContext.Proxies.FirstOrDefaultAsync(x => x.Id == proxy.Id);
                            dbContext.Proxies.Remove(proxyToDelete);
                            await dbContext.SaveChangesAsync();
                        }
                    }
                    catch(Exception ex)
                    {
                        _logger.Error(this.GetType(), ex.Message);
                    }
                });

                tasks.Add(task);
            }

            await Task.WhenAll(tasks);

            _logger.Info(this.GetType(), "Finish check old proxies");

            tasks = new List<Task>();

            var hidemyProxies = await _proxyHttpClient.GetProxiesAsync();

            foreach (var hidemyProxy in hidemyProxies)
            {
                var task = Task.Run(async () =>
                {
                    try
                    {
                        _logger.Info(this.GetType(), $"start new proxy {hidemyProxy.Ip} {hidemyProxy.Port}");
                        using var scope = _scopeFactory.CreateScope();

                        var dbContext = scope.ServiceProvider.GetRequiredService<IDbContext>();

                        var result = await _proxyHttpClient.CheckProxyAsync(hidemyProxy.Ip, hidemyProxy.Port);

                        if (result && await dbContext.Proxies.FirstOrDefaultAsync(x => x.Ip == hidemyProxy.Ip && x.Port == hidemyProxy.Port) == null)
                        {
                            var proxyToCreate = new Entities.Models.Proxy() { Ip = hidemyProxy.Ip, Port = hidemyProxy.Port };
                            await dbContext.Proxies.AddAsync(proxyToCreate);
                            await dbContext.SaveChangesAsync();
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.Error(this.GetType(), ex.Message);
                    }
                });

                tasks.Add(task);
            }

            await Task.WhenAll(tasks);

            _logger.Info(this.GetType(), "Finish check new proxies");

            _logger.Info(this.GetType(), "Finish UpdateProxiesJob");
        }
    }
}
