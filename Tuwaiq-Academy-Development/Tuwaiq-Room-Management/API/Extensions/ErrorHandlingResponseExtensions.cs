using Microsoft.AspNetCore.Mvc;
using Shared.Interfaces.ValidationErrors;

namespace API.Extensions;

public static class ErrorHandlingResponseExtensions
{
    public static ObjectResult Handle(this Error? error)
    {
        var resultError = error ?? Error.Empty();
        return new ObjectResult(resultError)
        {
            StatusCode = Error.GetStatus(resultError)
        };
    }
    
}