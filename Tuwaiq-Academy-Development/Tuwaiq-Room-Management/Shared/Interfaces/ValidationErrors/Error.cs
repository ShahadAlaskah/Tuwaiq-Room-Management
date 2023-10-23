namespace Shared.Interfaces.ValidationErrors;

public class Error
{
    public Error(string errorCode, string errorMessage)
    {
        ErrorCode = errorCode;
        ErrorMessage = errorMessage;
    }

    public string ErrorCode { get; }
    public string ErrorMessage { get; }
    public string RequestId { get; set; } = null!;

    public static Error Empty()
    {
        return new Error(string.Empty, string.Empty);
    }

    public static Error Create(string errorCode, string errorMessage)
    {
        return new Error(errorCode, errorMessage);
    }

    public static Error Create(string errorMessage)
    {
        return new Error("Error", errorMessage);
    }

    public static Error Invalid(string errorMessage)
    {
        return new Error("Invalid", errorMessage);
    }
    public static Error Required(string errorMessage)
    {
        return new Error("Invalid", errorMessage);
    }
    public static Error Already(string errorMessage)
    {
        return new Error("Already", errorMessage);
    }
    public static Error Exist(string errorMessage)
    {
        return new Error("Exist", errorMessage);
    }

    public static Error NotFound(string errorMessage)
    {
        return new Error("NotFound", errorMessage);
    }
    
    public static int GetStatus(Error error)
    {
        return error.ErrorCode switch
        {
            "Invalid" => 406,
            "NotFound" => 404,
            "Already" => 409,
            "Exist" => 409,
            _ => 400
        };
    }
}