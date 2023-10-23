using FluentValidation.Results;

namespace Shared.Interfaces.ValidationErrors;

public static class ErrorsExtensions
{
    public static Error[] CreateValidationError(this List<ValidationFailure> errors)
    {
        return errors.Select(s => Error.Create(s.PropertyName, s.ErrorMessage)).ToArray();
    }
}