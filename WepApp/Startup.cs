using Infrastructure.Implemtation.Bus;
using Infrastructure.Implemtation.Cian;
using Infrastructure.Implemtation.Cian.EventHandlers;
using Infrastructure.Implemtation.Cian.HttpClient;
using Infrastructure.Implemtation.Cian.Profiles;
using Infrastructure.Implemtation.DataAccess;
using Infrastructure.Implemtation.FileService;
using Infrastructure.Implemtation.Logger;
using Infrastructure.Implemtation.Polly;
using Infrastructure.Interfaces.Bus;
using Infrastructure.Interfaces.Cian;
using Infrastructure.Interfaces.Cian.HttpClient;
using Infrastructure.Interfaces.DataAccess;
using Infrastructure.Interfaces.FileService;
using Infrastructure.Interfaces.Jobs;
using Infrastructure.Interfaces.Logger;
using Infrastructure.Interfaces.Poll;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using UseCases.Flats.BackgroundJobs;
using UseCases.User.Base;
using UseCases.User.Commands.CreateUser;
using UseCases.User.Commands.SetFlatMinumPrice;
using UseCases.User.Commands.SetMaximumPrice;
using UseCases.User.Commands.SetTimeToMetro;
using UseCases.User.Queries.GetUser;
using UseCases.User.Queries.GetUserProfile;
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
            services.AddSingleton<IClientMessageSender, TelegramHttpClient>(x =>
            {
                return new TelegramHttpClient(Configuration.GetSection("ClientAppUrl").Get<string>());
            });
            services.AddTransient<IProxyManager, ProxyManager>();
          
            services.AddTransient<ICianService, CianService>();
            services.AddScoped<IPollService, PollingService>(x =>
            {
                return new PollingService(Configuration.GetSection("RetryCount").Get<int>(), 
                    x.GetRequiredService<ILoggerService>());
            });

            services.AddSingleton<IEventBus, InMemoryBus>();

            services.AddHostedService<CianSubscribers>();

            services.AddHostedService<JobsQueue>();

            //Jobs
            services.AddTransient<ParseCianRentFlatJob>();

            //Managers
            services.AddTransient<ISheduleJobManager, ParseCianJobManager>(x =>
            {
                return new ParseCianJobManager(
                    x.GetRequiredService<ILoggerService>(),
                    x.GetRequiredService<IServiceScopeFactory>(),
                    TimeSpan.FromHours(Configuration.GetSection("Jobs:ParseCianJobManager").Get<int>()));
            });

            //Mediatr
            services.AddMediatR(typeof(BaseUserRequest).Assembly);
            //EventHandlers
            services.AddTransient<HtmlDownloadHandler>();
            services.AddTransient<SendNotificationsHandler>();

            //frameworks
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
