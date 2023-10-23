using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestSharp;
using SDKIntegration.Models;

namespace SDKIntegration;



public class AuthorizationMessageClientCredentialsHandler : DelegatingHandler
{
    const string TokenName = "FormsIntegrationsSDKToken";

    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly MyFormsMemoryCache _myFormsMemoryCache;
    private readonly ILogger<AuthorizationMessageHandler> _logger;

    public AuthorizationMessageClientCredentialsHandler(IHttpContextAccessor httpContextAccessor,MyFormsMemoryCache myFormsMemoryCache,  ILogger<AuthorizationMessageHandler> logger)//
    {
        _httpContextAccessor = httpContextAccessor;
        _myFormsMemoryCache = myFormsMemoryCache;
        _logger = logger;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancelToken)
    {
        HttpRequestHeaders headers = request.Headers;

        AuthenticationHeaderValue authHeader = headers.Authorization;

        if (authHeader != null)
        {
            if (!_myFormsMemoryCache.Cache.TryGetValue(TokenName, out TokenResponse? cachedToken))
            {
                var _settings = _httpContextAccessor!.HttpContext!.RequestServices
                    .GetRequiredService<TuwaiqFormsApiSettings>();
                var options = new RestClientOptions(_settings.IdentityServerBaseUrl)
                {
                    MaxTimeout = -1,
                };
                var client = new RestClient(options);
                var rr = new RestRequest(_settings.IdentityServerTokenUrl, Method.Post);
                rr.AddHeader("content-type", "application/x-www-form-urlencoded");
                rr.AddParameter("grant_type", "client_credentials");
                rr.AddParameter("client_id", _settings.ClientId);
                rr.AddParameter("client_secret", _settings.ClientSecret);
                _settings.Scopes?.ToList().ForEach(s => rr.AddParameter("scope", s));
            
                var response = client.Post<TokenResponse>(rr);
                _logger.LogInformation($"Response: {JsonConvert.SerializeObject(response, Formatting.Indented)}");
            
                cachedToken = response;
            
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddSeconds(response?.ExpiresIn - 20 ?? 100))
                    .SetPriority(CacheItemPriority.High)
                    .SetSlidingExpiration(TimeSpan.FromSeconds(response?.ExpiresIn - 20 ?? 100))
                    .SetSize(1);
            
                _myFormsMemoryCache.Cache.Set(TokenName, cachedToken, cacheEntryOptions);
            }
            _logger.LogInformation($"Token: {JsonConvert.SerializeObject(cachedToken, Formatting.Indented)}");
            headers.Authorization = new AuthenticationHeaderValue(authHeader.Scheme, cachedToken?.AccessToken);

            // var tokenAsync = await _httpContextAccessor.HttpContext!.GetTokenAsync("access_token");
            // _logger.LogInformation("Token: {TokenAsync}", tokenAsync);
            // headers.Authorization = new AuthenticationHeaderValue(authHeader.Scheme, tokenAsync);
        }

        return await base.SendAsync(request, cancelToken);
    }
}


public class AuthorizationMessageHandler : DelegatingHandler
{
    // const string TokenName = "FormsIntegrationsSDKToken";

    private readonly IHttpContextAccessor _httpContextAccessor;
    // private readonly MyFormsMemoryCache _myFormsMemoryCache;
    private readonly ILogger<AuthorizationMessageHandler> _logger;

    public AuthorizationMessageHandler(IHttpContextAccessor httpContextAccessor, ILogger<AuthorizationMessageHandler> logger)//MyFormsMemoryCache myFormsMemoryCache, 
    {
        _httpContextAccessor = httpContextAccessor;
        // _myFormsMemoryCache = myFormsMemoryCache;
        _logger = logger;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancelToken)
    {
        HttpRequestHeaders headers = request.Headers;

        AuthenticationHeaderValue authHeader = headers.Authorization;

        if (authHeader != null)
        {
            // if (!_myFormsMemoryCache.Cache.TryGetValue(TokenName, out TokenResponse? cachedToken))
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
            //     _logger.LogInformation($"Response: {JsonConvert.SerializeObject(response, Formatting.Indented)}");
            //
            //     cachedToken = response;
            //
            //     var cacheEntryOptions = new MemoryCacheEntryOptions()
            //         .SetAbsoluteExpiration(DateTime.Now.AddSeconds(response?.ExpiresIn - 20 ?? 100))
            //         .SetPriority(CacheItemPriority.High)
            //         .SetSlidingExpiration(TimeSpan.FromSeconds(response?.ExpiresIn - 20 ?? 100))
            //         .SetSize(1);
            //
            //     _myFormsMemoryCache.Cache.Set(TokenName, cachedToken, cacheEntryOptions);
            // }
            // _logger.LogInformation($"Token: {JsonConvert.SerializeObject(cachedToken, Formatting.Indented)}");
            // headers.Authorization = new AuthenticationHeaderValue(authHeader.Scheme, cachedToken?.AccessToken);

            // var tokenAsync = await _httpContextAccessor.HttpContext!.GetTokenAsync("access_token");
            // _logger.LogInformation("Token: {TokenAsync}", tokenAsync);
            // headers.Authorization = new AuthenticationHeaderValue(authHeader.Scheme, tokenAsync);
        }

        return await base.SendAsync(request, cancelToken);
    }
}