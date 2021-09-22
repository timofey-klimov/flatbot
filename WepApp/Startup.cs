using Infrastructure.Implemtation.Bus;
using Infrastructure.Implemtation.Cian;
using Infrastructure.Implemtation.Cian.EventHandlers;
using Infrastructure.Implemtation.Cian.HttpClient;
using Infrastructure.Implemtation.Cian.Profiles;
using Infrastructure.Implemtation.DataAccess;
using Infrastructure.Implemtation.FileService;
using Infrastructure.Implemtation.Logger;
using Infrastructure.Implemtation.Polly;
using Infrastructure.Implemtation.Telegram;
using Infrastructure.Interfaces.Bus;
using Infrastructure.Interfaces.Cian;
using Infrastructure.Interfaces.Cian.HttpClient;
using Infrastructure.Interfaces.DataAccess;
using Infrastructure.Interfaces.FileService;
using Infrastructure.Interfaces.Jobs;
using Infrastructure.Interfaces.Logger;
using Infrastructure.Interfaces.Poll;
using Infrastructure.Interfaces.Telegram;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using UseCases.Flats.BackgroundJobs;
using UseCases.Notifications.Jobs;
using UseCases.User.Base;
using UseCases.User.Queries.Profiles;
using WepApp.HostedServices.EventBusSubscribers;
using WepApp.HostedServices.Queue;
using WepApp.JobManagers;
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

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
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
            services.AddScoped<IFIleShare, LocalFileShare>();
            services.AddTransient<ICianHttpClient, CianHttpClient>();
            services.AddSingleton<ITelegramMessageSender, TelegramMessageSender>(x =>
            {
                return new TelegramMessageSender(Configuration.GetSection("ClientAppUrl").Get<string>());
            });
            services.AddTransient<IProxyManager, ProxyManager>();
          
            services.AddTransient<ICianService, CianService>();
            services.AddScoped<IPollService, PollingService>(x =>
            {
                return new PollingService(Configuration.GetSection("RetryCount").Get<int>(), 
                    x.GetRequiredService<ILoggerService>());
            });

            services.AddSingleton<IEventBus, InMemoryBus>();

            services.AddScoped<ITelegramNotificationService, TelegramNotificationsService>();

            //EventBustSubsribers
            services.AddHostedService<CianSubscribers>();


            //Jobs
            services.AddTransient<ParseCianRentFlatJob>();
            services.AddTransient<SendEveryDayFlatsNotificationJob>();
            services.AddTransient<SendEveryWeekFlatsNotificationJob>();
            services.AddHostedService<JobsQueue>();

            //Managers
            services.AddTransient<ISheduleJobManager, ParseCianJobManager>(x =>
            {
                return new ParseCianJobManager(
                    x.GetRequiredService<ILoggerService>(),
                    x.GetRequiredService<IServiceScopeFactory>(),
                    Configuration.GetSection("Jobs:ParseCianJobManager").Get<int>());
            });

            services.AddTransient<ISheduleJobManager, SendEveryDayFlatsNotificationManager>(x =>
            {
                return new SendEveryDayFlatsNotificationManager(
                    x.GetRequiredService<ILoggerService>(),
                    x.GetRequiredService<IServiceScopeFactory>(),
                    Configuration.GetSection("Jobs:SendEveryDayFlatsNotification").Get<int>());
            });

            services.AddTransient<ISheduleJobManager, SendEveryWeekFlatsNotificationManager>(x =>
            {
                return new SendEveryWeekFlatsNotificationManager(
                    x.GetRequiredService<ILoggerService>(),
                    x.GetRequiredService<IServiceScopeFactory>(),
                    Configuration.GetSection("Jobs:SendEveryWeekFlatsNotification").Get<int>());
            });

            //EventHandlers
            services.AddTransient<HtmlDownloadHandler>();
            services.AddTransient<SendNotificationsHandler>();


            //frameworks
            services.AddMediatR(typeof(BaseUserRequest).Assembly);
            services.AddControllers().AddNewtonsoftJson();
            services.AddAutoMapper(
                typeof(ProxyProfile),
                typeof(UserProfile));
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
        }
    }
}
