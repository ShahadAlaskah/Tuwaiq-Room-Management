using Microsoft.Extensions.DependencyInjection;
using Refit;

namespace SDKIntegration;

public static class FormsApiExtensions
{
    public static IServiceCollection SetupFormsApi(this IServiceCollection services, string url)
    {
        services.AddHttpContextAccessor();

        // services.AddSingleton<MyFormsMemoryCache>();
        // services.AddSingleton<TuwaiqFormsApiSettings>(tuwaiqFormsApiSettings);

        services.AddTransient<AuthorizationMessageHandler>();

        services
            .AddRefitClient<IFormsApi>()
            .ConfigureHttpClient(client =>
            {
                client.BaseAddress = new Uri(url);
            })
            .AddHttpMessageHandler<AuthorizationMessageHandler>()
            ;

        services
            .AddRefitClient<ILookUpApi>()
            .ConfigureHttpClient(client =>
            {
                client.BaseAddress = new Uri(url);
            })
            .AddHttpMessageHandler<AuthorizationMessageHandler>()
            ;

        return services;
    }
    
    
    public static IServiceCollection SetupFormsApi(this IServiceCollection services, TuwaiqFormsApiSettings tuwaiqFormsApiSettings)
    {
        services.AddHttpContextAccessor();

        services.AddSingleton<MyFormsMemoryCache>();
        services.AddSingleton<TuwaiqFormsApiSettings>(tuwaiqFormsApiSettings);

        services.AddTransient<AuthorizationMessageClientCredentialsHandler>();

        services
            .AddRefitClient<IFormsApi>()
            .ConfigureHttpClient(client =>
            {
                client.BaseAddress = new Uri(tuwaiqFormsApiSettings.Url);
            })
            .AddHttpMessageHandler<AuthorizationMessageClientCredentialsHandler>()
            ;

        services
            .AddRefitClient<ILookUpApi>()
            .ConfigureHttpClient(client =>
            {
                client.BaseAddress = new Uri(tuwaiqFormsApiSettings.Url);
            })
            .AddHttpMessageHandler<AuthorizationMessageClientCredentialsHandler>()
            ;

        return services;
    }
}