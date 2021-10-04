using Microsoft.AspNetCore.Mvc;
using WepApp.Dto;

namespace WepApp.Controllers.Base
{
    public class BaseApiController : ControllerBase
    {
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
