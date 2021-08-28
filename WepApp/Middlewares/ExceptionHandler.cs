using Entities.Models.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using WepApp.Dto;

namespace WepApp.Middlewares
{
    public class ExceptionHandler
    {
        private readonly RequestDelegate _next;

        public ExceptionHandler(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var loggerFactory = context.RequestServices.GetRequiredService<ILoggerFactory>();
            var logger = loggerFactory.CreateLogger<ExceptionHandler>();
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);

                string response = string.Empty;

                if (ex is ExceptionBase baseEx)
                {
                    response = JsonConvert.SerializeObject(ApiResponse.Fail(baseEx.Message));
                }
                else
                {
                    response = JsonConvert.SerializeObject(ApiResponse.Fail("Unhandled exeption"));
                }

                await context.Response.WriteAsync(response);
            }
        }
    }
}
