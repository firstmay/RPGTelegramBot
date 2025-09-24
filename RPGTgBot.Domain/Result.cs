namespace RPGTgBot.Domain
{
    public class Result
    {
        public bool IsSuccess { get; private set; }
        public string Error { get; private set; }

        private Result(bool isSuccess,string error)
        {
            IsSuccess = isSuccess;
            Error = error;
        }

        public static Result Ok() => new Result(true, string.Empty);
        public static Result Fail(string error) => new Result(false, error);
    }

    public class Result<T>
    {
        public bool IsSuccess { get; private set; }
        public string Error { get; private set; }
        public T? Value { get; private set; }

        private Result(bool isSuccess, string error, T? value)
        {
            IsSuccess = isSuccess;
            Error = error;
            Value = value;
        }

        public static Result<T> Ok(T value) => new(true, string.Empty, value);
        public static Result<T> Fail (string error) => new(false, ResultPattern.ValidateErrorMsg(error), default);
    }

    public static class ResultPattern
    {
        public static string ValidateErrorMsg(string error)
        {
            return string.IsNullOrWhiteSpace(error) ? "Неизвестная ошибка" : error;
        }
    }
}
