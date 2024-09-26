namespace MicroMarinCase.Application.Wrappers
{
    public class Result
    {
        public bool IsSuccess { get; private set; }
        public string Message { get; private set; }

        public Result(string message, bool isSuccess = true)
        {
            Message = message;
            IsSuccess = isSuccess;
        }

        public static Result Success(string message)
        {
            return new(message);
        }

        public static Result Error(string message)
        {
            return new(message, false);
        }
    }
    public class Result<T> where T : class
    {
        public T Data { get; private set; }
        public bool IsSuccess { get; private set; }
        public string Message { get; private set; }

        public Result(T data, string message, bool isSuccess = true)
        {
            Data = data;
            Message = message;
            IsSuccess = isSuccess;
        }

        public static Result<T> Success(T data, string message)
        {
            return new(data, message);
        }

        public static Result<T> Error(T data, string message)
        {
            return new(data, message, false);
        }
    }
}
