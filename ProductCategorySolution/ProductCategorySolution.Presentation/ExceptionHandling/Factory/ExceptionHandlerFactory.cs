using Microsoft.Extensions.Logging;
using ProductCategorySolution.Presentation.ExceptionHandling.ExceptionHandlers;
using ProductCategorySolution.Presentation.ExceptionHandling.ExceptionHandlers.Base;
using System;

namespace ProductCategorySolution.Presentation.ExceptionHandling.Factory
{
    /// <summary>
    /// Factory for creating correct exception handler for exception middleware.
    /// </summary>
    public class ExceptionHandlerFactory : IExceptionHandlerFactory
    {
        private readonly ILoggerFactory _loggerFactory;

        public ExceptionHandlerFactory(ILoggerFactory loggerFactory) => _loggerFactory = loggerFactory;

        public AbsExceptionHandler Create(Exception exception)
        {
            var logger = _loggerFactory.CreateLogger(exception.GetType().Name);

            if (exception.GetType() == typeof(ArgumentNullException))
                return new ArgumentNullExceptionHandler(exception, logger);
            else if (exception.GetType() == typeof(UnauthorizedAccessException))
                return new UnauthorizedAccessExceptionHandler(exception, logger);
            else
                return new GeneralExceptionHandler(exception, logger);
        }
    }
}
