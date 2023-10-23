using Shared.Interfaces.ValidationErrors;

namespace Shared.Exceptions;

public class FormsNotFoundException : Exception
{
    public FormsNotFoundException(string errorMessage) : base(errorMessage)
    {
        Error = Error.NotFound(errorMessage);
    }

    public Error Error { get; set; }
}