using Microsoft.AspNetCore.Http;
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

        public AbsExceptionHandler(Exception exception) => _exception = exception;

        public virtual async Task HandleAsync(HttpContext context)
        {
            //Loglama

            context.Response.ContentType = "application/json";

            await context.Response.WriteAsync(JsonConvert.SerializeObject(ErrorResultModel));
        }
    }
}
