using ProductCategorySolution.Presentation.ExceptionHandling.ExceptionHandlers;
using ProductCategorySolution.Presentation.ExceptionHandling.ExceptionHandlers.Base;
using System;

namespace ProductCategorySolution.Presentation.ExceptionHandling
{
    public static class ExceptionHandlerFactory
    {
        public static AbsExceptionHandler Create(Exception exception)
        {
            if (exception.GetType() == typeof(ArgumentNullException))
                return new ArgumentNullExceptionHandler(exception);
            else if (exception.GetType() == typeof(UnauthorizedAccessException))
                return new UnauthorizedAccessExceptionHandler(exception);
            else
                return new GeneralExceptionHandler(exception);
        }
    }
}
