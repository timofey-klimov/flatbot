using Hangfire;
using Infrastructure.Implemtation.Bus;
using Infrastructure.Implemtation.Cian;
using Infrastructure.Implemtation.Cian.Events.ExcelDownloaded;
using Infrastructure.Implemtation.Cian.HttpClient;
using Infrastructure.Implemtation.DataAccess;
using Infrastructure.Implemtation.FileService;
using Infrastructure.Implemtation.Logger;
using Infrastructure.Implemtation.Polly;
using Infrastructure.Interfaces.Bus;
using Infrastructure.Interfaces.Cian;
using Infrastructure.Interfaces.Cian.HttpClient;
using Infrastructure.Interfaces.DataAccess;
using Infrastructure.Interfaces.FileService;
using Infrastructure.Interfaces.Logger;
using Infrastructure.Interfaces.Poll;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using UseCases.Flats.BackgroundJobs;
using WepApp.HostedServices.EventBusSubscribers;
using WepApp.Middlewares;
using WepApp.Services;

namespace WepApp
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var serviceFactory = new ServiceFactory();
            //Infrastructure
            services.AddDbContext<IDbContext, FlatDbContext>(x =>
            {
                x.UseSqlServer(Configuration.GetConnectionString("FlatDb"));
            });
            services.AddScoped<ICianUrlBuilder, CianUrlBuilder>();

            services.AddSingleton<ICianMapManager, CianMapManager>(x =>
            {
                return serviceFactory.CreateMapManager(Configuration, x);
            });

            services.AddSingleton<ILoggerService, LoggerService>(x =>
            {
                return serviceFactory.CreateLogger(x);
            });
            services.AddScoped<IFIleShare, LocalFileShare>();
            services.AddScoped<ICianHttpClient, CianHttpClient>();
            services.AddScoped<ICianService, CianService>();
            services.AddScoped<IProxyManager, ProxyManager>(x =>
            {
                return serviceFactory.CreateProxyManager(Configuration);
            });
            services.AddScoped<IPollService, PollingService>(x =>
            {
                return new PollingService(3, x.GetRequiredService<ILoggerService>());
            });

            services.AddSingleton<IEventBus, InMemoryBus>();

            services.AddHostedService<CianExcelParserHostedService>();

            //Events
            services.AddTransient<ExcelDownloadHandler>();

            //frameworks
            services.AddControllers();
            services.AddHangfire(x =>
            {
                x.UseSerilogLogProvider();
                x.UseSqlServerStorage(Configuration.GetConnectionString("HangfireConnection"));
            });
            services.AddHangfireServer();

            services.AddScoped<ParseCianRentFlatJob>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware<ExceptionHandler>();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            RecurringJob.AddOrUpdate<ParseCianRentFlatJob>(x => x.Execute(), Cron.Hourly);
        }
    }
}
