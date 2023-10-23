using Shared.Interfaces.ValidationErrors;

namespace Shared.Exceptions;

public class FormsException : Exception
{
    public FormsException(Error? error) : base(error?.ErrorMessage??"")
    {
        Error = error;
    }

    public Error? Error { get; set; }
}