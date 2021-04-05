namespace Application.Core
{
    public class AppException
    {
        // details = null - so we do not have to supply a value in PRODUCTION MODE
        public AppException(int statusCode, string message, string details = null)
        {
            StatusCode = statusCode;
            Message = message;
            Details = details;
        }

        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string Details { get; set; }
    }
}