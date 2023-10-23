using System.Diagnostics.CodeAnalysis;
using Shared.Interfaces.ValidationErrors;

namespace Shared.Base;

public class Result
{
    protected Result(bool success, Error? error = null)
    {
        Success = success;
        Error = error;
        if (!(success || error != null)) throw new Exception("CannotBeSuccessAndErrorHasValue");

        if (!(!success || error == null)) throw new Exception("CannotBeNotSuccessAndErrorDoesNNotHaveValue");
    }

    protected Result(bool success, List<Error>? errors = null)
    {
        Success = success;
        Errors = errors;
        if (!(success || errors != null)) throw new Exception("CannotBeSuccessAndErrorHasValue");

        if (!(!success || errors == null)) throw new Exception("CannotBeNotSuccessAndErrorDoesNNotHaveValue");
    }

    public bool Success { get; }
    public Error? Error { get; }
    public List<Error>? Errors { get; } = new List<Error>();

    public bool Failure => !Success || Errors?.Any() == true || Error != null;

    public static Result Fail(Error? message)
    {
        return new Result(false, message);
    }

    public static Result Fail(List<Error>? messages)
    {
        return new Result(false, messages);
    }

    public static Result<T> Fail<T>(Error? error)
    {
        return new Result<T>(default, false, error);
    }

    public static Result<T> Fail<T>(List<Error>? errors)
    {
        return new Result<T>(default, false, errors);
    }

    public static Result Ok()
    {
        return new Result(true, error: default!);
    }

    public static Result<T> Ok<T>(T value)
    {
        return new Result<T>(value, true, error: default!);
    }

    public static Result Combine(params Result[] results)
    {
        foreach (var result in results)
            if (result.Failure)
                return result;

        return Ok();
    }
}

public class Result<T> : Result
{
    private T _value = default!;

    protected internal Result([AllowNull] T value, bool success, Error? error)
        : base(success, error)
    {
        if (!(value != null || !success)) throw new Exception("ValueMustHaveValueOrNotSuccess");

        Value = value!;
    }

    protected internal Result([AllowNull] T value, bool success, List<Error>? errors)
        : base(success, errors)
    {
        if (!(value != null || !success)) throw new Exception("ValueMustHaveValueOrNotSuccess");

        Value = value!;
    }

    public T Value
    {
        get
        {
            if (!Success) throw new Exception("MustBeSuccess");

            return _value;
        }
        [param: AllowNull] private set => _value = value;
    }
}