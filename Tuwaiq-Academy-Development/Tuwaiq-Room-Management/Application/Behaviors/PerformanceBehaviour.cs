using System.Diagnostics;
using Shared.Extensions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.Behaviors;

public class PerformanceBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    // private readonly ICurrentUserResolver _currentUserService;
    private readonly ILogger<TRequest> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly Stopwatch _timer;

    public PerformanceBehaviour(
        ILogger<TRequest> logger, IHttpContextAccessor httpContextAccessor)
    {
        _timer = new Stopwatch();

        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
        // _currentUserService = currentUserService;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        _timer.Start();

        var response = await next();

        _timer.Stop();

        var elapsedMilliseconds = _timer.ElapsedMilliseconds;

        if (elapsedMilliseconds > 100)
        {
            var requestName = typeof(TRequest).Name;
            var userName = _httpContextAccessor.HttpContext.User?.GetUsername();

            if (_logger.IsEnabled(LogLevel.Warning))
            {
                _logger.LogWarning("Long Running Request: {Name} ({ElapsedMilliseconds} milliseconds) {@UserName}",
                    requestName, elapsedMilliseconds, userName);
            }
        }

        return response;
    }
}