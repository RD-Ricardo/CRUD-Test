namespace CrossCutting.Utils
{
    public class Error
    {
        public string Message { get; set; }
        public ErrorTypeEnum Type { get; set; }
        public Error(string message, ErrorTypeEnum type)
        {
            Message = message;
            Type = type;
        }
    }

    public enum ErrorTypeEnum
    {
        NotFound = 0,
        BadRequest = 1,
        Validation = 2,
        InternalServerError = 3
    }
}
