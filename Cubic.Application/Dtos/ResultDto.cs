using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Cubic.Application.Dtos
{
    public class ResultDto
    {
        protected ResultDto()
        {
            CreatedDate = DateTime.UtcNow;
        }
        public bool IsSucceeded { get; set; }
        public string Message { get; set; }
        public int DataCount { get; set; }
        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    }

    public class Result<T> : ResultDto
    {
        public T Data { get; set; }
        public static Result<T> Success(T data)
        {
            return new Result<T>()
            {
                Data = data,
                IsSucceeded = true,

            };
        }
        public static Result<T> Success(T data, int totalCount, string message)
        {
            return new Result<T>()
            {
                Data = data,
                IsSucceeded = true,
                Message = message,
                DataCount = totalCount,

            };
        }
        public static Result<T> Success(T data, string message, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            return new Result<T>()
            {
                Data = data,
                Message = message,
                IsSucceeded = true,
                StatusCode = statusCode
            };
        }


        public static Result<T> Failed(string message, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            return new Result<T>()
            {
                Message = message,
                IsSucceeded = false,
                StatusCode = statusCode


            };
        }

        public static Result<T> Failed(T data, string message, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            return new Result<T>()
            {
                Data = data,
                Message = message,
                IsSucceeded = false,
                StatusCode = statusCode

            };
        }
    }
}
