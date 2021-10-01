using Microsoft.AspNetCore.Http;
using ProductCategory.Service;
using ProductCategorySolution.Presentation.ExceptionHandling.ExceptionHandlers.Base;
using ProductCategorySolution.Presentation.ExceptionHandling.Models;
using System;
using System.Net;
using System.Threading.Tasks;

namespace ProductCategorySolution.Presentation.ExceptionHandling.ExceptionHandlers
{
    public class UnauthorizedAccessExceptionHandler : AbsExceptionHandler
    {
        public UnauthorizedAccessExceptionHandler(Exception exception) : base(exception)
        {
        }

        public override async Task HandleAsync(HttpContext context)
        {
            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            ErrorResultModel = new ErrorResultModel(ErrorMessageConstants.Unauthorized);

            await base.HandleAsync(context);
        }
    }
}
