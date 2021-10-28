using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using WepApp.Dto;

namespace WepApp.Controllers.Base
{
    public class BaseApiController : ControllerBase
    {
        protected IMediator Mediator;
        public BaseApiController(IMediator mediator)
        {
            if (mediator == null)
                throw new ArgumentNullException(nameof(IMediator));

            Mediator = mediator;
        }

        protected ApiResponse Ok()
        {
            return ApiResponse.Success();
        }

        protected ApiResponse<T> Ok<T>(T value)
        {
            return ApiResponse<T>.Success(value);
        }
    }
}
