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
}
