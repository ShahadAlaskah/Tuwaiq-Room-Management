//using FluentValidation;
//using MediatR;
//using System.Collections.Generic;
//using System.Reflection;
//using Microsoft.Extensions.Logging;
//using API.Shared.Base;
//using API.Shared.Interfaces;
//using API.Shared.Interfaces.ValidationErrors;
//using API.Shared.API.Application API.Application.Behaviors;

//public class AuditPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
//{
//    private readonly ILogger<AuditPipelineBehavior<TRequest, TResponse>> _logger;
//    private readonly ISender _sender;

//    //public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
//    //{
//    //    if (_validators?.Any()==false)
//    //        return await next();

//    //    var errors = _validators.Select(s => s.Validate(request))
//    //        .SelectMany(s => s.Errors)
//    //        .Where(s => s is not null)
//    //        .Select(s => new Error(s.PropertyName, s.ErrorMessage))
//    //        .Distinct()
//    //        .ToArray();

//    //    if (errors.Any()) return CreateValidationResult<TResponse>(errors);

//    //    return await next();
//    //}

//    //private TResult CreateValidationResult<TResult>(Error[] errors) where TResult : Result
//    //{
//    //    if (typeof(TResult) == typeof(Result)) return (ValidationResult.WithErrors(errors) as TResult)!;

//    //    var validationResult = typeof(ValidationResult<>)
//    //        .GetGenericTypeDefinition()
//    //        .MakeGenericType(typeof(TResult).GenericTypeArguments[0])
//    //        .GetMethod(nameof(ValidationResult.WithErrors))!
//    //        .Invoke(null, new object?[] { errors })!;

//    //    return (TResult)validationResult;
//    //}

//    public AuditPipelineBehavior(ILogger<AuditPipelineBehavior<TRequest, TResponse>> logger,ISender sender)
//    {
//        _logger = logger;
//        _sender = sender;
//    }

//    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
//    {
//        var baseType = typeof(IQuery<>);
//        var type = request.GetType().GetInterface(baseType.Name)?.GetGenericTypeDefinition();
//        //var tt = typeof(IQuery<>).GetGenericTypeDefinition()
//        //    .MakeGenericType(type);
//        if (type != null && type == baseType)
//        {
//            //var s = request.GetType().GetGenericTypeDefinition().GetGenericArguments().First();
//            var props = request.GetType().GetProperties();
//            var items = new Dictionary<string, string?>();
//            foreach (var prop in props)
//            {
//                items.Add(prop.Name, request.GetType().GetProperty(prop.Name)?.GetValue(request, null)?.ToString()?.Split('.').LastOrDefault());
//            }
//            //var page = props.Any(c => c.Name == "Page") ? s.GetProperty("Page").GetValue(request, null) : "1";
//            //var pageCount = props.Any(c => c.Name == "Page") ? s.GetProperty("PageCount").GetValue(request, null) : "1";


//            await _sender.Send(new CreateAuditViewCommand(AuditViewType.View,_httpContext?.User.GetUserId(),"Query ReferenceFloors By Id", new[] { "ReferenceFloors" }, new Dictionary<string, object>() { { "Page", 1 }, { "PageCount", 1 } }), cancellationToken);

//            _logger.LogInformation("Values {0}", string.Join(",", items.Select(x => $"{x.Key}: {x.Value}")));
//        }

//        return await next();
//    }
//}

