using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ProductCategorySolution.Presentation.ExceptionHandling.Models;
using System;
using System.Threading.Tasks;

namespace ProductCategorySolution.Presentation.ExceptionHandling.ExceptionHandlers.Base
{
    public abstract class AbsExceptionHandler
    {
        protected ErrorResultModel ErrorResultModel;

        private Exception _exception;

        ILogger _logger;

        public AbsExceptionHandler(Exception exception, ILogger logger)
        {
            _exception = exception;
            _logger = logger;
        }

        public virtual async Task HandleAsync(HttpContext context)
        {
            _logger.LogError(_exception, string.Empty);

            context.Response.ContentType = "application/json";

            await context.Response.WriteAsync(JsonConvert.SerializeObject(ErrorResultModel));
        }
    }
}
