using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace ProductCategorySolution.Presentation.ExceptionHandling
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next) => _next = next;

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                var handler = ExceptionHandlerFactory.Create(ex);
                await handler.HandleAsync(httpContext);
            }
        }
    }
}
