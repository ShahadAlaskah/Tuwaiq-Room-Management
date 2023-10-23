using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Infrastructure.Persistence.Ef.Implementations;

namespace Infrastructure.Persistence.Ef;
//
//
// public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
// {
//     private readonly IDomainEventDispatcher _dispatcher;
//     private readonly ICurrentUserResolver _userResolver;
//
//     public ApplicationDbContextFactory()
//     {
//         
//     }
//     public ApplicationDbContextFactory(IDomainEventDispatcher dispatcher, ICurrentUserResolver userResolver)
//     {
//         _dispatcher = dispatcher;
//         _userResolver = userResolver;
//     }
//
//     public ApplicationDbContext CreateDbContext(string[] args)
//     {
//         string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
//
//         // Build config
//         IConfiguration configuration = new ConfigurationBuilder()
//             .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../API.WebUI"))
//             .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
//             .AddJsonFile($"appsettings.{environment}.json", optional: true)
//             .AddEnvironmentVariables()
//             .Build();
//
//         var connectionString = //"Data Source=app.db";
//             configuration.GetConnectionString("DefaultConnection");
//
//         var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
//
//         optionsBuilder.UseSqlServer(connectionString
//                 , s => s.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)
//                     .EnableRetryOnFailure(3)
//                     .CommandTimeout(180))
//             .EnableDetailedErrors().EnableSensitiveDataLogging();
//
//         return new ApplicationDbContext(optionsBuilder.Options,_dispatcher,_userResolver);
//     }
// }
//
// public class AudiViewDbContextFactory : IDesignTimeDbContextFactory<AudiViewDbContext>
// {
//     private readonly IDomainEventDispatcher _dispatcher;
//     private readonly ICurrentUserResolver _userResolver;
//
//     public AudiViewDbContextFactory()
//     {
//         
//     }
//     
//     public AudiViewDbContextFactory(IDomainEventDispatcher dispatcher, ICurrentUserResolver userResolver)
//     {
//         _dispatcher = dispatcher;
//         _userResolver = userResolver;
//     }
//     public AudiViewDbContext CreateDbContext(string[] args)
//     {
//         // Get environment
//         string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
//         
//         // Build config
//         IConfiguration configuration = new ConfigurationBuilder()
//             .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../API.WebUI"))
//             .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
//             .AddJsonFile($"appsettings.{environment}.json", optional: true)
//             .AddEnvironmentVariables()
//             .Build();
//
//         var connectionString = //"Data Source=app.db";
//             configuration.GetConnectionString("DefaultConnection");
//
//         var optionsBuilder = new DbContextOptionsBuilder<AudiViewDbContext>();
//
//         optionsBuilder.UseSqlServer(connectionString
//                 , s => s.MigrationsAssembly(typeof(AudiViewDbContext).Assembly.FullName)
//                     .EnableRetryOnFailure(3)
//                     .CommandTimeout(180))
//             .EnableDetailedErrors().EnableSensitiveDataLogging();
//
//         return new AudiViewDbContext(optionsBuilder.Options,_dispatcher,_userResolver);
//     }
// }

public static class PersistenceExtensions
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = //"Data Source=app.db";
            configuration.GetConnectionString("DefaultConnection");


        services.AddDbContext<ApplicationDbContext>(options =>
                options //.ReplaceService<IValueConverterSelector, StronglyTypedIdValueConverterSelector>()
                    .UseMySql(connectionString, new MySqlServerVersion(configuration.GetValue<string>("MySqlServerVersion")), x =>
                    x.CommandTimeout(180)
                //.MigrationsHistoryTable("__MyMigrationsHistory", "dbo")
            )
//                     .UseSqlServer(connectionString
//                         , s => s.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)
//                             .EnableRetryOnFailure(3)
//                             .CommandTimeout(180).MigrationsHistoryTable("__MyMigrationsHistory", "dbo"))
//                     .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
// #if DEBUG
//                     .EnableDetailedErrors().EnableSensitiveDataLogging().EnableThreadSafetyChecks()
// #endif
        // .UseModel(API.Infrastructure.Persistence.Ef.ApplicationDbContextModel.Instance)
        );
//
//
//         services.AddDbContext<AudiViewDbContext>(options =>
//             options //.ReplaceService<IValueConverterSelector, StronglyTypedIdValueConverterSelector>()
//                 .UseSqlServer(connectionString
//                     , s => s.MigrationsAssembly(typeof(AudiViewDbContext).Assembly.FullName)
//                         .EnableRetryOnFailure(3).CommandTimeout(180))
// #if DEBUG
//                         .EnableDetailedErrors().EnableSensitiveDataLogging()
// #endif    
//             );


        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped(typeof(IGenericRepositoryCurd<>), typeof(GenericRepositoryCurd<>));
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));


        return services;
    }
}