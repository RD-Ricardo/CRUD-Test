namespace CrossCutting.Utils
{
    public class Result<T>
    {
        public bool IsSuccess { get; set; }
        public T? Data { get; set; }
        public Error[]? Errors { get; set; }

        public static Result<T> Success(T Data)
        {
            return new Result<T>
            {
                IsSuccess = true,
                Data = Data
            };
        }

        public static Result<T> Failure(Error error)
        {
            return new Result<T>
            {
                IsSuccess = false,
                Errors = [error]
            };
        }

        public static Result<T> Failure(Error[] errors)
        {
            return new Result<T>
            {
                IsSuccess = false,
                Errors = errors
            };
        }
    }
}
