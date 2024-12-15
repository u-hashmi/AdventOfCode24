namespace Helpers;

public class Result<T, TE> where TE: Error
{
    public T Value { get; }
    public TE? Error { get; }
    public bool IsSuccess => Error == null;

    private Result(T value, TE? error)
    {
        Value = value;
        Error = error;
    }

    public static Result<T, TE> From(T value, TE? error = null)
    {
        return error == null
            ? new Result<T, TE>(value, null)
            : new Result<T, TE>(default!, error);
    }
}