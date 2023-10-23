using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace SDK;


public class AuthorizationMessageHandler : DelegatingHandler
{
    const string Formssdktoken = "FormsSDKToken";

    private readonly IHttpContextAccessor _httpContextAccessor;
    // private readonly MyMemoryCache _myMemoryCache;
    private readonly ILogger<AuthorizationMessageHandler> _logger;

    public AuthorizationMessageHandler(IHttpContextAccessor httpContextAccessor,  ILogger<AuthorizationMessageHandler> logger)//MyMemoryCache myMemoryCache,
    {
        _httpContextAccessor = httpContextAccessor;
        // _myMemoryCache = myMemoryCache;
        _logger = logger;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancelToken)
    {
        HttpRequestHeaders headers = request.Headers;

        AuthenticationHeaderValue authHeader = headers.Authorization;

        if (authHeader != null)
        {
            // if (!_myMemoryCache.Cache.TryGetValue(Formssdktoken, out TokenResponse? cachedToken))
            // {
            //     var _settings = _httpContextAccessor!.HttpContext!.RequestServices
            //         .GetRequiredService<TuwaiqFormsApiSettings>();
            //     var options = new RestClientOptions(_settings.IdentityServerBaseUrl)
            //     {
            //         MaxTimeout = -1,
            //     };
            //     var client = new RestClient(options);
            //     var rr = new RestRequest(_settings.IdentityServerTokenUrl, Method.Post);
            //     rr.AddHeader("content-type", "application/x-www-form-urlencoded");
            //     rr.AddParameter("grant_type", "client_credentials");
            //     rr.AddParameter("client_id", _settings.ClientId);
            //     rr.AddParameter("client_secret", _settings.ClientSecret);
            //     _settings.Scopes?.ToList().ForEach(s => rr.AddParameter("scope", s));
            //
            //     var response = client.Post<TokenResponse>(rr);
            //     cachedToken = response;
            //
            //     var cacheEntryOptions = new MemoryCacheEntryOptions()
            //         .SetAbsoluteExpiration(DateTime.Now.AddSeconds(response?.ExpiresIn - 20 ?? 100))
            //         .SetPriority(CacheItemPriority.High)
            //         .SetSlidingExpiration(TimeSpan.FromSeconds(response?.ExpiresIn - 20 ?? 100))
            //         .SetSize(1);
            //
            //     _myMemoryCache.Cache.Set(Formssdktoken, cachedToken, cacheEntryOptions);
            // }
            // _logger.LogInformation($"Token: {JsonConvert.SerializeObject(cachedToken, Formatting.Indented)}");
            // headers.Authorization = new AuthenticationHeaderValue(authHeader.Scheme, cachedToken?.AccessToken);
            headers.Authorization = new AuthenticationHeaderValue(authHeader.Scheme, await _httpContextAccessor.HttpContext?.GetTokenAsync("access_token"));
        }

        return await base.SendAsync(request, cancelToken);
    }
}