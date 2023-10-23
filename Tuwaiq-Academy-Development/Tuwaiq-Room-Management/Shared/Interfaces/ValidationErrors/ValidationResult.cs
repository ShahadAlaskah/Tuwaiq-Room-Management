using Shared.Base;

namespace Shared.Interfaces.ValidationErrors;

public class ValidationResult : Result, IValidationResult
{
    private ValidationResult(Error[] errors) : base(false, IValidationResult.ValidationError)
    {
        Errors = errors;
    }

    public Error[] Errors { get; } = null!;

    public static ValidationResult WithErrors(Error[] errors)
    {
        return new ValidationResult(errors);
    }
}

public class ValidationResult<TResponse> : Result<TResponse>, IValidationResult
{
    private ValidationResult(Error[] errors) : base(default, false, IValidationResult.ValidationError)
    {
        Errors = errors;
    }

    public Error[] Errors { get; }

    public static ValidationResult<TResponse> WithErrors(Error[] errors)
    {
        return new ValidationResult<TResponse>(errors);
    }
}