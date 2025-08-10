namespace Application.Common
{
    public sealed record Result(bool Succeeded, string? Message = null, string[]? Errors = null)
    {
        public static Result Ok(string? message = null) => new(true, message, null);
        public static Result Fail(params string[] errors) => new(false, null, errors);
    }

    public sealed record Result<T>(bool Succeeded, T? Value = default, string? Message = null, string[]? Errors = null)
    {
        public static Result<T> Ok(T value, string? message = null) => new(true, value, message, null);
        public static Result<T> Fail(params string[] errors) => new(false, default, null, errors);
    }
}
