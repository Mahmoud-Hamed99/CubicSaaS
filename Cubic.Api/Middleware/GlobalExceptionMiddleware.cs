using Cubic.Application.Dtos;
using System.Net.Sockets;

namespace Cubic.Api.Middleware
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
      
        public GlobalExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
             
                if (context.Response.StatusCode == 401 && !context.Response.HasStarted)
                {
                    context.Response.ContentType = "application/json";
                   
                     await context.Response.WriteAsJsonAsync(Result<bool>.Failed("Unauthorized — valid token is required",System.Net.HttpStatusCode.Unauthorized));

                }
                else if (context.Response.StatusCode == 403 && !context.Response.HasStarted)
                {
                    context.Response.ContentType = "application/json";
                  
                    await context.Response.WriteAsJsonAsync(Result<bool>.Failed("Forbidden — you don't have permission to access this resource", System.Net.HttpStatusCode.Forbidden));

                }
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = 500;
                context.Response.ContentType = "application/json";

                var response = new
                {
                    message = ex.Message
                };

                await context.Response.WriteAsJsonAsync(response);
            }
        }
    }
}
