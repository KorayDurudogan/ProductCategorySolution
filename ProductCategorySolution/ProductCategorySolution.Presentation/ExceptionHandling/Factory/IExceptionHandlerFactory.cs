using ProductCategorySolution.Presentation.ExceptionHandling.ExceptionHandlers.Base;
using System;

namespace ProductCategorySolution.Presentation.ExceptionHandling.Factory
{
    /// <summary>
    /// Interface for creating necessary exception handler for the specific error type.
    /// </summary>
    public interface IExceptionHandlerFactory
    {
        AbsExceptionHandler Create(Exception exception);
    }
}
