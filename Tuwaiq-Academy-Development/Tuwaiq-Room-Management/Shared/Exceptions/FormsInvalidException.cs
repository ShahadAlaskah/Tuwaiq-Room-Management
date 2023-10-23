using Shared.Interfaces.ValidationErrors;

namespace Shared.Exceptions;

public class FormsInvalidException : Exception
{
    public FormsInvalidException(string errorMessage) : base(errorMessage)
    {
        Error = Error.Invalid(errorMessage);
    }

    public Error Error { get; set; }
}