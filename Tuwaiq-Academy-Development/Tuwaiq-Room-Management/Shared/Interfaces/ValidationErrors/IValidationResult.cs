namespace Shared.Interfaces.ValidationErrors;

public interface IValidationResult
{
    public static readonly Error? ValidationError = new("ValidationError", "A Validation Problem Occurred");
    Error[] Errors { get; }
}