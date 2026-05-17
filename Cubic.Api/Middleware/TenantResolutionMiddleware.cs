using Cubic.Application.Implmentations;
using Cubic.Core.Interfaces;

namespace Cubic.Api.Middleware
{
    public class TenantResolutionMiddleware
    {
        private readonly RequestDelegate _next;
        
        public TenantResolutionMiddleware(RequestDelegate next)
        {
            _next = next;
        }


        public async Task InvokeAsync(HttpContext context, ITenantContext tenantContext)
        {
            if (context.Request.Path.StartsWithSegments("/api/user"))
            {
                var header = context.Request.Headers["X-Tenant-Id"].FirstOrDefault();

                if (string.IsNullOrEmpty(header) || !Guid.TryParse(header, out var tenantId))
                {
                    context.Response.StatusCode = 400;
                    await context.Response.WriteAsJsonAsync(new
                    {
                        status = 400,
                        error = "X-Tenant-Id header is missing or invalid"
                    });
                    return;
                }

                ((TenantContext)tenantContext).TenantId = tenantId;
                await _next(context);
                return;              
            }

            await _next(context);    
        }
    }
    }
