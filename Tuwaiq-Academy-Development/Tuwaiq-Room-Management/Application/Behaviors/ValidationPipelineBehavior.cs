using FluentValidation;
using MediatR;
using Shared.Base;
using Shared.Interfaces.ValidationErrors;
using ValidationResult = Shared.Interfaces.ValidationErrors.ValidationResult;

namespace Application.Behaviors;

public class ValidationPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : Result
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationPipelineBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (!_validators.Any())
            return await next();

        var errors = _validators.Select(s => s.Validate(request))
            .SelectMany(s => s.Errors)
            .Where(s => s is not null)
            .Select(s => new Error(s.PropertyName, s.ErrorMessage))
            .Distinct()
            .ToArray();

        if (errors.Any())
        {
           return CreateValidationResult<TResponse>(errors);
        }

        return await next();
    }

    private TResult CreateValidationResult<TResult>(Error[] errors) where TResult : Result
    {
        if (typeof(TResult) == typeof(Result)) return (ValidationResult.WithErrors(errors) as TResult)!;

        var validationResult = typeof(ValidationResult<>)
            .GetGenericTypeDefinition()
            .MakeGenericType(typeof(TResult).GenericTypeArguments[0])
            .GetMethod(nameof(ValidationResult.WithErrors))!
            .Invoke(null, new object?[] { errors })!;
        
        return (TResult)validationResult;
    }
}