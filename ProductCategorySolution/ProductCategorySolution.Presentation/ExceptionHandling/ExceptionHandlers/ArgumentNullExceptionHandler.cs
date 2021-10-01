using Microsoft.AspNetCore.Http;
using ProductCategory.Service;
using ProductCategorySolution.Presentation.ExceptionHandling.ExceptionHandlers.Base;
using ProductCategorySolution.Presentation.ExceptionHandling.Models;
using System;
using System.Net;
using System.Threading.Tasks;

namespace ProductCategorySolution.Presentation.ExceptionHandling.ExceptionHandlers
{
    public class ArgumentNullExceptionHandler : AbsExceptionHandler
    {
        public ArgumentNullExceptionHandler(Exception exception) : base(exception)
        {
        }

        public override async Task HandleAsync(HttpContext context)
        {
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            ErrorResultModel = new ErrorResultModel(ErrorMessageConstants.BadRequest);

            await base.HandleAsync(context);
        }
    }
}
