using Shared.Correlation;

namespace API.Extensions;

public class RequestHandler : DelegatingHandler
{
    private readonly ICorrelationIdAccessor _correlationIdAccessor;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public RequestHandler(ICorrelationIdAccessor correlationIdAccessor, IHttpContextAccessor httpContextAccessor)
    {
        this._correlationIdAccessor = correlationIdAccessor;
        this._httpContextAccessor = httpContextAccessor;
    }

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        request.Headers.Add("CorrelationId", _correlationIdAccessor.CorrelationId); // Getting correlationid from request context. 
        return base.SendAsync(request, cancellationToken);
    }
}