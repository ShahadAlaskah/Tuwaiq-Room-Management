//using FluentValidation.Results;

//namespace API.Shared.Interfaces.ValidationErrors;

//public static class ProblemDetailsExtensions
//{

//    public static ProblemDetails Create(string title, string errorMessage, int code, string? url=null, object? errors = null)
//    {
//        var problemDetails = new ProblemDetails()
//        {
//            Title = title,
//            Detail = errorMessage,
//            Status = code,
//            Type = "https://www.tawthiqconnect.moj.gov.sa/errors/httpstatuses/" + code,
//            Instance = url,
//        };
//        if (errors is not null)
//        {
//            problemDetails = new ProblemDetails()
//            {
//                Title = title,
//                Detail = errorMessage,
//                Status = code,
//                Type = "https://www.tawthiqconnect.moj.gov.sa/errors/httpstatuses/" + code,
//                Instance = url,
//                Extensions = { { "errors", errors } }
//            };
//        }
//        return problemDetails;
//    }

//    public static ProblemDetails CreateError(Error error) => Create(error.ErrorCode, error.ErrorMessage, StatusCodes.Status400BadRequest);
//    public static ProblemDetails CreateError(string errorMessage) => Create("Error", errorMessage, StatusCodes.Status400BadRequest);
//    public static ProblemDetails CreateInvalid(string errorMessage) => Create("Invalid", errorMessage, StatusCodes.Status406NotAcceptable);
//    public static ProblemDetails CreateNotFound(string errorMessage) => Create("NotFound", errorMessage, StatusCodes.Status404NotFound);
//    public static ProblemDetails CreateValidationError(Error[] errors) => Create("ValidationError", "ValidationError", StatusCodes.Status406NotAcceptable,errors:errors);
//    public static ProblemDetails CreateValidationError(List<ValidationFailure> errors) => Create("ValidationError", "ValidationError", StatusCodes.Status406NotAcceptable,errors:errors.Select(s=> Error.Create(s.PropertyName,s.PropertyName)).ToArray());
//}

