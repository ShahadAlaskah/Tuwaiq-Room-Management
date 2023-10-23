using Shared.Exceptions;
using Shared.Interfaces.ValidationErrors;

namespace Shared.Base;

public static class ResultExtensions
{
    public static Result OnSuccess(this Result result, Func<Result> func)
    {
        return result.Failure ? result : func();
    }

    public static Result OnSuccess(this Result result, Action action)
    {
        if (result.Failure)
            return result;

        action();

        return Result.Ok();
    }

    public static Result OnSuccess<T>(this Result<T> result, Action<T> action)
    {
        if (result.Failure)
            return result;

        action(result.Value);

        return Result.Ok();
    }

    public static Result<T> OnSuccess<T>(this Result result, Func<T> func)
    {
        if (result.Failure)
            return Result.Fail<T>(result.Error);

        return Result.Ok(func());
    }

    public static Result<T> OnSuccess<T>(this Result result, Func<Result<T>> func)
    {
        if (result.Failure)
            return Result.Fail<T>(result.Error);

        return func();
    }

    public static Result OnSuccess<T>(this Result<T> result, Func<T, Result> func)
    {
        if (result.Failure)
            return result;

        return func(result.Value);
    }

    public static Result OnFailure(this Result result, Action action)
    {
        if (result.Failure) action();

        return result;
    }

    public static Result OnBoth(this Result result, Action<Result> action)
    {
        action(result);

        return result;
    }

    public static T OnBoth<T>(this Result result, Func<Result, T> func)
    {
        return func(result);
    }


    public static Exception HandleFailure(this Result result)
    {
        return result switch
        {
            { Success: true } => throw new InvalidOperationException(),
            IValidationResult validationResult => throw new FormsValidationException(validationResult.Errors),
            { Success: false } => throw new FormsException(result.Error!),
            //    throw new ProblemDetailsException(new ProblemDetails()
            //{
            //    Title = result.Error!.ErrorCode,
            //    Status = result.Error.ErrorCode== "NotFound"? StatusCodes.Status404NotFound : result.Error.ErrorCode == "Invalid" ? StatusCodes.Status406NotAcceptable :  StatusCodes.Status500InternalServerError,
            //    Type = result.Error?.ErrorCode,
            //    Detail = result.Error?.ErrorMessage,
            //}),
            _ =>
                throw new FormsException(result.Error!)
        };
    }

    //private static ProblemDetails CreateProblemDetails(string title, int status, Error? error, Error[]? errors = null)
    //{
    //    return new ProblemDetails
    //    {
    //        Title = title,
    //        Status = status,
    //        Type = error?.ErrorCode,
    //        Detail = error?.ErrorMessage,
    //        Extensions = { { nameof(errors), errors } }
    //    };
    //}
}