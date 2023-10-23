using Microsoft.Extensions.DependencyInjection;
using Refit;

namespace SDK;

public static class RoomManagementApiExtensions
{
    public static IServiceCollection SetupFormsApi(this IServiceCollection services, TuwaiqRoomManagementApiSettings tuwaiqRoomManagementApiSettings)
    {
        services.AddHttpContextAccessor();

        services.AddSingleton<MyMemoryCache>();
        services.AddSingleton<TuwaiqRoomManagementApiSettings>(tuwaiqRoomManagementApiSettings);

        services.AddTransient<AuthorizationMessageHandler>();

        services
            .AddRefitClient<IAssetTypesApi>()
            .ConfigureHttpClient(client =>
            {
                client.BaseAddress = new Uri(tuwaiqRoomManagementApiSettings.Url);
            })
            .AddHttpMessageHandler<AuthorizationMessageHandler>()
            ;

        services
            .AddRefitClient<IBuildingsApi>()
            .ConfigureHttpClient(client =>
            {
                client.BaseAddress = new Uri(tuwaiqRoomManagementApiSettings.Url);
            })
            .AddHttpMessageHandler<AuthorizationMessageHandler>()
            ;

        services
            .AddRefitClient<IFilesApi>()
            .ConfigureHttpClient(client =>
            {
                client.BaseAddress = new Uri(tuwaiqRoomManagementApiSettings.Url);
            })
            .AddHttpMessageHandler<AuthorizationMessageHandler>()
            ;

        services
            .AddRefitClient<IFloorsApi>()
            .ConfigureHttpClient(client =>
            {
                client.BaseAddress = new Uri(tuwaiqRoomManagementApiSettings.Url);
            })
            .AddHttpMessageHandler<AuthorizationMessageHandler>()
            ;
        services
            .AddRefitClient<ILookupApi>()
            .ConfigureHttpClient(client =>
            {
                client.BaseAddress = new Uri(tuwaiqRoomManagementApiSettings.Url);
            })
            .AddHttpMessageHandler<AuthorizationMessageHandler>()
            ;
        services
            .AddRefitClient<IRoomsApi>()
            .ConfigureHttpClient(client =>
            {
                client.BaseAddress = new Uri(tuwaiqRoomManagementApiSettings.Url);
            })
            .AddHttpMessageHandler<AuthorizationMessageHandler>()
            ;
        services
            .AddRefitClient<IRoomTypesApi>()
            .ConfigureHttpClient(client =>
            {
                client.BaseAddress = new Uri(tuwaiqRoomManagementApiSettings.Url);
            })
            .AddHttpMessageHandler<AuthorizationMessageHandler>()
            ;

        return services;
    }
    
    public static IServiceCollection SetupFormsApi(this IServiceCollection services, string url)
    {
        services.AddHttpContextAccessor();

        services.AddTransient<AuthorizationMessageHandler>();

        services
            .AddRefitClient<IAssetTypesApi>()
            .ConfigureHttpClient(client =>
            {
                client.BaseAddress = new Uri(url);
            })
            .AddHttpMessageHandler<AuthorizationMessageHandler>()
            ;

        services
            .AddRefitClient<IBuildingsApi>()
            .ConfigureHttpClient(client =>
            {
                client.BaseAddress = new Uri(url);
            })
            .AddHttpMessageHandler<AuthorizationMessageHandler>()
            ;

        services
            .AddRefitClient<IFilesApi>()
            .ConfigureHttpClient(client =>
            {
                client.BaseAddress = new Uri(url);
            })
            .AddHttpMessageHandler<AuthorizationMessageHandler>()
            ;

        services
            .AddRefitClient<IFloorsApi>()
            .ConfigureHttpClient(client =>
            {
                client.BaseAddress = new Uri(url);
            })
            .AddHttpMessageHandler<AuthorizationMessageHandler>()
            ;
        
        services
            .AddRefitClient<ILookupApi>()
            .ConfigureHttpClient(client =>
            {
                client.BaseAddress = new Uri(url);
            })
            .AddHttpMessageHandler<AuthorizationMessageHandler>()
            ;
        
        services
            .AddRefitClient<IRoomsApi>()
            .ConfigureHttpClient(client =>
            {
                client.BaseAddress = new Uri(url);
            })
            .AddHttpMessageHandler<AuthorizationMessageHandler>()
            ;
        
        services
            .AddRefitClient<IRoomTypesApi>()
            .ConfigureHttpClient(client =>
            {
                client.BaseAddress = new Uri(url);
            })
            .AddHttpMessageHandler<AuthorizationMessageHandler>()
            ;

        return services;
    }
}