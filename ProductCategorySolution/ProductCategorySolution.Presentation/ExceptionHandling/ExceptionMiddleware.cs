using Microsoft.AspNetCore.Http;
using ProductCategorySolution.Presentation.ExceptionHandling.Factory;
using System;
using System.Threading.Tasks;

namespace ProductCategorySolution.Presentation.ExceptionHandling
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly IExceptionHandlerFactory _exceptionHandlerFactory;

        public ExceptionMiddleware(RequestDelegate next, IExceptionHandlerFactory exceptionHandlerFactory)
        {
            _next = next;
            _exceptionHandlerFactory = exceptionHandlerFactory;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                var handler = _exceptionHandlerFactory.Create(ex);
                await handler.HandleAsync(httpContext);
            }
        }
    }
}
