namespace ProductCategorySolution.Presentation.ExceptionHandling.Models
{
    /// <summary>
    /// Model for returning general expceptions.
    /// </summary>
    public class ErrorResultModel
    {
        public ErrorResultModel(string message) => Message = message;

        public string Message { get; set; }
    }
}
