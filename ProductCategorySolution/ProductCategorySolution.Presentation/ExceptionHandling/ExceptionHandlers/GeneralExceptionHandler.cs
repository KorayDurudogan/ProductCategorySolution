using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using ProductCategory.Service;
using ProductCategorySolution.Presentation.ExceptionHandling.ExceptionHandlers.Base;
using ProductCategorySolution.Presentation.ExceptionHandling.Models;
using System;
using System.Net;
using System.Threading.Tasks;

namespace ProductCategorySolution.Presentation.ExceptionHandling.ExceptionHandlers
{
    public class GeneralExceptionHandler : AbsExceptionHandler
    {
        public GeneralExceptionHandler(Exception exception, ILogger logger)
            : base(exception, logger)
        {
        }

        public override async Task HandleAsync(HttpContext context)
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            ErrorResultModel = new ErrorResultModel(ErrorMessageConstants.General);

            await base.HandleAsync(context);
        }
    }
}
