using Entities.Exceptions.Base;
using Entities.Models.Exceptions;
using Infrastructure.Interfaces.Logger;
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
            var logger = context.RequestServices.GetRequiredService<ILoggerService>();
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                logger.Error(this.GetType(), ex.Message);

                var apiResponse = CreateApiResponse(ex);

                await context.Response.WriteAsync(JsonConvert.SerializeObject(apiResponse));
            }
        }

        private static ApiResponse CreateApiResponse(Exception ex)
        {
            if (ex is ExceptionBase baseEx)
            {
                var apiResponse = baseEx switch
                {
                    EntityNotFoundExceptionBase notFoundEx => ApiResponse.Fail(notFoundEx.Message, 400),

                    _ => ApiResponse.Fail(baseEx.Message, 500)
                };

                return apiResponse;
            }
            else
            {
                return ApiResponse.FailInternal();
            }
        }
    }
}
