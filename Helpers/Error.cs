namespace Helpers;

public record Error
{
    public string ErrorMessage { get; }
    public ErrorTypes ErrorType { get; }

    private Error(ErrorTypes errorType, string errorMessage)
    {
        ErrorType = errorType;
        ErrorMessage = errorMessage;
    }

    public static Error Create(ErrorTypes errorType, string errorMessage) 
        => new Error(errorType, errorMessage);
}