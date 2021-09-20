using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WepApp.Dto
{
    public class ApiResponse
    {
        public int Code { get; private set; }

        public string Message { get; private set; }

        public ApiResponse(int code, string message)
        {
            Code = code;
            Message = message;
        }

        public static ApiResponse Success(string message) => new ApiResponse(0, message);

        public static ApiResponse Fail(string message) => new ApiResponse(500, message);

        public static ApiResponse Success() => new ApiResponse(0, string.Empty);

    }

    public class ApiResponse<T> : ApiResponse
    {
        public T Data { get; private set; }

        public ApiResponse(T data, int code, string message)
            :base(code, message)
        {
            Data = data;
        }

        public static ApiResponse<T> Success(T data, string message) => new ApiResponse<T>(data, 0, message);

        public static ApiResponse<T> Fail(string message) => new ApiResponse<T>(default, 500, message);

        public static ApiResponse<T> Success(T data) => new ApiResponse<T>(data, 0, string.Empty);
    }
}
