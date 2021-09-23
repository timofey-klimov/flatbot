using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WepApp.Extensions
{
    public static class AddSwagger
    {
        public static void AddSwaggerDi(this IServiceCollection services)
        {
            services.AddSwaggerGen(cfg => 
            {
                cfg.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Version = "v1",
                    Title = "Bot API"
                });
            });
        }
    }
}
