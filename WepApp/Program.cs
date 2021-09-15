using Infrastructure.Interfaces.DataAccess;
using Infrastructure.Interfaces.Logger;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;

namespace WepApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            ILoggerService logger = default;
            try
            {
                var host = CreateHostBuilder(args).Build();
                logger = CreateLogger(host.Services);
                await ApplyMigrations(host.Services, logger);
                host.Run();
            }
            catch (Exception ex)
            {
                logger?.Error(ex.Message);
            }
        }

        private static ILoggerService CreateLogger(IServiceProvider services)
        {
            using var scope = services.CreateScope();
            return scope.ServiceProvider.GetRequiredService<ILoggerService>();
        }

        private static async Task ApplyMigrations(IServiceProvider services, ILoggerService logger)
        {
            using var scope = services.CreateScope();
            using var db = scope.ServiceProvider.GetRequiredService<IDbContext>();
            await db.Database.MigrateAsync();
            logger.Info("Migrate succesfully");
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
