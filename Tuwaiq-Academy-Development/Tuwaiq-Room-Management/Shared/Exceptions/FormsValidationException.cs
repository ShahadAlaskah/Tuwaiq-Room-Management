using Shared.Interfaces.ValidationErrors;

namespace Shared.Exceptions;

public class FormsValidationException : Exception
{
    public FormsValidationException(Error[] errors)
    {
        Errors = errors;
    }

    public Error[] Errors { get; set; }
}