using Infrastructure.Implemtation.BitmapManger;
using Infrastructure.Implemtation.Bus;
using Infrastructure.Implemtation.Cian;
using Infrastructure.Implemtation.Cian.EventHandlers;
using Infrastructure.Implemtation.Cian.HttpClient;
using Infrastructure.Implemtation.Cian.Profiles;
using Infrastructure.Implemtation.Common;
using Infrastructure.Implemtation.Common.EventHandlers;
using Infrastructure.Implemtation.DataAccess;
using Infrastructure.Implemtation.JsonConverters;
using Infrastructure.Implemtation.Logger;
using Infrastructure.Implemtation.Polly;
using Infrastructure.Implemtation.Proxy.HttpClient;
using Infrastructure.Implemtation.Telegram;
using Infrastructure.Implemtation.Telegram.Factory;
using Infrastructure.Implemtation.Telegram.HostManager;
using Infrastructure.Interfaces.BitmapManager;
using Infrastructure.Interfaces.Bus;
using Infrastructure.Interfaces.Cian;
using Infrastructure.Interfaces.Cian.HttpClient;
using Infrastructure.Interfaces.Common;
using Infrastructure.Interfaces.DataAccess;
using Infrastructure.Interfaces.Logger;
using Infrastructure.Interfaces.Poll;
using Infrastructure.Interfaces.Proxy.HttpClient;
using Infrastructure.Interfaces.Telegram;
using Infrastructure.Interfaces.Telegram.HostManager;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using UseCases.District.Profiles;
using UseCases.Flats.BackgroundJobs;
using UseCases.Notifications.Jobs;
using UseCases.Proxies.Jobs;
using UseCases.User.Base;
using UseCases.User.Queries.Profiles;
using WepApp.Extensions;
using WepApp.HostedServices.EventBusSubscribers;
using WepApp.HostedServices.SheduleManager;
using WepApp.JobManagers;
using WepApp.JobManagers.Profile;
using WepApp.Middlewares;

namespace WepApp
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var serviceFactory = new Services.ServiceFactory();

            services.AddSingleton(Configuration);

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
            services.AddTransient<ICianHttpClient, CianHttpClient>();
            services.AddSingleton<ITelegramMessageSender, TelegramMessageSender>(x =>
            {
                return new TelegramMessageSender(
                    Configuration.GetSection("ClientAppUrl").Get<string>(),
                    x.GetRequiredService<ILoggerService>());
            });
            services.AddTransient<IProxyManager, ProxyManager>();
            services.AddTransient<INotificationCreatorFactory, NotificationCreatorFactrory>();
          
            services.AddTransient<IParseCianHtmlManager, ParseCianHtmlManager>();
            services.AddScoped<IPollService, PollingService>(x =>
            {
                return new PollingService(Configuration.GetSection("RetryCount").Get<int>(), 
                    x.GetRequiredService<ILoggerService>());
            });

            services.AddSingleton<IEventBus, InMemoryBus>();

            services.AddScoped<IFilterFlatService, FilterFlatService>();
            services.AddSingleton<IFlatCountInMessageManager, FlatCountInMessageManager>(x =>
            {
                return new FlatCountInMessageManager() { FlatCount = Configuration.GetSection("FlatCountInMessage").Get<int>() };
            });
            services.AddScoped<IImageManager, ImageManager>();
            services.AddScoped<IProxyHttpClient, ProxyHttpClient>();
            services.AddTransient<IFinderCianFlatsByHtml, FinderCianFlatsByHtml>();
            services.AddTransient<ICianFlatJsonParser, CianFlatJsonParser>();
            services.AddTransient<ICianFlatJsonCreator, CianFlatJsonCreator>();
            services.AddTransient<ICianFlatsCreator, CianFlatsCreator>();
            services.AddSingleton<ITelegramClientHostManager, TelegramClientHostManager>(x =>
            {
                return new TelegramClientHostManager(Configuration.GetSection("ClientAppUrl").Get<string>());
            });

            //EventBustSubsribers
            services.AddHostedService<BusSubscribers>();
            //SheduleJobManager
            services.AddHostedService<StartSheduleJobs>();

            //Jobs
            services.AddTransient<ParseCianRentFlatJob>();
            services.AddTransient<SendEveryDayFlatsNotificationJob>();
            services.AddTransient<SendEveryWeekFlatsNotificationJob>();
            services.AddTransient<UpdateProxiesJob>();

            //Managers
            services.AddTransient<ParseCianJobManager>();
            services.AddTransient<SendEveryDayFlatsNotificationManager>();
            services.AddTransient<SendEveryWeekFlatsNotificationManager>();
            services.AddTransient<UpdateProxiesJobManager>();

            //EventHandlers
            services.AddTransient<SendNotificationsHandler>();
            services.AddTransient<NewFlatCreatedHandler>();


            //frameworks
            services.AddMediatR(typeof(BaseUserRequest).Assembly);
            services.AddControllers()
                .AddNewtonsoftJson(x => x.SerializerSettings.Converters.Add(new MemoryStreamJsonConverter()));
                
            services.AddAutoMapper(
                typeof(ProxyProfile),
                typeof(UserProfile),
                typeof(DistrictProfile),
                typeof(JobManagerProfile));

            services.AddMemoryCache();
            services.AddSwaggerDi();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware<ExceptionHandler>();

            app.UseSwagger(c =>
            {
                c.SerializeAsV2 = true;
            });

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Bot Api");
            });

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
