using Infrastructure.Implemtation.Cian;
using Infrastructure.Implemtation.Cian.HttpClient;
using Infrastructure.Implemtation.DataAccess;
using Infrastructure.Implemtation.Logger;
using Infrastructure.Interfaces.Cian;
using Infrastructure.Interfaces.Cian.HttpClient;
using Infrastructure.Interfaces.DataAccess;
using Infrastructure.Interfaces.Logger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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
            services.AddScoped<ICianUrlBuilder, CianUrlBuilder>(x =>
            {
                return serviceFactory.CreateCianUlrBuilder(Configuration, x);
            });

            services.AddSingleton<ILoggerService, LoggerService>(x =>
            {
                return serviceFactory.CreateLogger(x);
            });

            services.AddScoped<ICianHttpClient, CianHttpClient>();
            services.AddControllers();
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
