using System.Globalization;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using API.Extensions;
using API.HealthChecks;
using API.Helpers;
using API.Settings;
using Application.Behaviors;
using Application.Commands.AuditViews.CommandHandlers;
using Application.Commands.AuditViews.Commands;
using Application.Dto;
using Application.Interfaces;
using Application.Persistence.Queries;
using Application.Search;
using BT.Logger.Abstraction;
using ExpressionDebugger;
using FluentValidation;
using FluentValidation.AspNetCore;
using Imageflow.Server.HybridCache;
using Infrastructure.Persistence.Ef;
using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Logging;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Humanizer;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.OpenApi.Models;
using Serilog;
using Quartz;
using Quartz.Impl.AdoJobStore;
using Shared;
using Shared.Correlation;
using Shared.Domain;
using JsonSerializerOptions = System.Text.Json.JsonSerializerOptions;
using Hellang.Middleware.ProblemDetails;
using Infrastructure.Persistence.QueryHandlers;
using OpenIddict.Validation.AspNetCore;

try
{
    var supportedCultures = new[] { new CultureInfo("ar-EG") };

    var builder = WebApplication.CreateBuilder(args);

    var root = builder.Environment.ContentRootPath;
    if (!Directory.Exists(Path.Combine(root, "Storage"))) Directory.CreateDirectory(Path.Combine(root, "Storage"));
    if (!Directory.Exists(Path.Combine(root, "Storage", "Search")))
        Directory.CreateDirectory(Path.Combine(root, "Storage", "Search"));
    if (!Directory.Exists(Path.Combine(root, "Storage", "Files")))
        Directory.CreateDirectory(Path.Combine(root, "Storage", "Files"));
    if (!Directory.Exists(Path.Combine(root, "Storage", "Images")))
        Directory.CreateDirectory(Path.Combine(root, "Storage", "Images"));
    if (!Directory.Exists(Path.Combine(root, "Storage", "Images", "cache")))
        Directory.CreateDirectory(Path.Combine(root, "Storage", "Images", "cache"));

    IdentityModelEventSource.ShowPII = true;

    var connectionString = //"Data Source=app.db";
        builder.Configuration.GetConnectionString("DefaultConnection");

    builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

    builder.Services.AddLoggerServices(builder.Configuration, connectionString!, builder.Host, "Logs");


    // builder.Services.Configure<RequestLocalizationOptions>(options =>
    // {
    //     var cultureInfo = supportedCultures.First();
    //     // cultureInfo.DateTimeFormat.DateSeparator = "-";
    //     // cultureInfo.DateTimeFormat.ShortDatePattern = "dd-MM-yyyy";
    //     options.DefaultRequestCulture = new RequestCulture(cultureInfo);
    //     var optionsSupportedCultures = supportedCultures.Select(s => s).ToList();
    //     options.SupportedCultures = optionsSupportedCultures;
    //     options.SupportedUICultures = optionsSupportedCultures;
    // });

    builder.Services.AddScoped<LanguageActionFilter>();

    builder.Services.Configure<RequestLocalizationOptions>(
        options =>
        {
            var cult = new RequestCulture(culture: "ar-EG", uiCulture: "ar-EG");
            cult.Culture.NumberFormat.CurrencySymbol = "SAR";
            cult.Culture.DateTimeFormat.ShortDatePattern = "dd-MM-yyyy";

            options.DefaultRequestCulture = cult;
            options.SupportedCultures = supportedCultures;
            options.SupportedUICultures = supportedCultures;
            options.RequestCultureProviders = new[]
            {
                new CookieRequestCultureProvider()
                {
                    /*CookieName = "BadawiTECH.Edu.Admin.Culture"*/
                }
            };
        });

    builder.Services.AddScoped<ValidateMimeMultipartContentFilter>();

    builder.Services.Configure<Shared.Settings.CacheSettings>(builder.Configuration.GetSection(nameof(Shared.Settings.CacheSettings)));
    builder.Services.AddOptions<Shared.Settings.CacheSettings>()
        .BindConfiguration(nameof(Shared.Settings.CacheSettings))
        .ValidateOnStart()
        ;


    builder.Services.Configure<MessageBrokerSettings>(builder.Configuration.GetSection(nameof(MessageBrokerSettings)));
    builder.Services.AddSingleton(sp => sp.GetRequiredService<IOptions<MessageBrokerSettings>>().Value);

    builder.Services.AddQuartz(q =>
    {
        q.SchedulerId = "Scheduler-Core";
        q.UseMicrosoftDependencyInjectionJobFactory();
        q.UseSimpleTypeLoader();
        // q.UseInMemoryStore();
        q.UseDefaultThreadPool(tp => { tp.MaxConcurrency = 10; });
        q.UseTimeZoneConverter();
        q.UsePersistentStore(d =>
        {
            d.UseProperties = true;
            d.UseJsonSerializer();
            d.UseMySql(providerOptions =>
            {
                providerOptions.UseDriverDelegate<MySQLDelegate>();
                providerOptions.ConnectionString = connectionString;
                providerOptions.TablePrefix = "QRTZ_";
            });
        });
    });

    builder.Services.AddQuartzHostedService(options => { options.WaitForJobsToComplete = true; });

// base configuration from appsettings.json
    builder.Services.Configure<QuartzOptions>(builder.Configuration.GetSection("Quartz"));

// if you are using persistent job store, you might want to alter some options
    builder.Services.Configure<QuartzOptions>(options =>
    {
        options.Scheduling.IgnoreDuplicates = true; // default: false
        options.Scheduling.OverWriteExistingData = true; // default: true
    });

    builder.Services.AddMediatR(configuration =>
    {
        configuration.RegisterServicesFromAssembly(typeof(Program).Assembly);
        configuration.RegisterServicesFromAssembly(typeof(CreateAuditViewCommand).Assembly);
        configuration.RegisterServicesFromAssembly(typeof(GetAuditQuery).Assembly);
        configuration.RegisterServicesFromAssembly(typeof(CreateAuditViewCommandHandler).Assembly);
        configuration.RegisterServicesFromAssembly(typeof(GetAuditQueryHandler).Assembly);
        configuration.Lifetime = ServiceLifetime.Scoped;
    });

    builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));
    builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));

    builder.Services.AddTransient<IDomainEventDispatcher, MediatrDomainEventDispatcher>();

// builder.Services.AddScoped<ICurrentUserResolver, CurrentUserResolver>();


    builder.Services.AddFluentValidationAutoValidation();
    builder.Services.AddFluentValidationClientsideAdapters();
    builder.Services.AddValidatorsFromAssemblyContaining(typeof(CreateAuditViewCommand));

    builder.Services.AddPersistenceServices(builder.Configuration);

    // builder.Services.AddTransient<IValidatorInterceptor, UseCustomErrorModelInterceptor>();

    
    builder.Services.AddCors(options =>
    {
        // options.AddPolicy(CorsPolicyName, configure =>
        // {
        //     configure
        //         .WithHeaders(
        //             //Microsoft minimum set recommended 
        //             "Accept", "Content-Type", "Origin", 
        //             //Swagger headers
        //             "api_key", "authorization", "x-requested-with" )
        //         .WithOrigins();
        // });

        options.AddPolicy("MyAllowSpecificOrigins",
            policy =>
            {
                var origins = builder.Configuration.GetValue<string>("AllowedHosts")?.Split(';');
                if (origins != null)
                    policy.WithOrigins(origins).AllowAnyHeader()
                        .AllowAnyMethod().WithExposedHeaders("Content-Disposition");
            });
    });

    builder.Services
        .AddControllersWithViews(options =>
        {
            options.OutputFormatters.RemoveType<SystemTextJsonOutputFormatter>();
            options.OutputFormatters.Add(new SystemTextJsonOutputFormatter(
                new JsonSerializerOptions(JsonSerializerDefaults.Web)
                {
                    ReferenceHandler = ReferenceHandler.Preserve
                }));
        }).AddRazorRuntimeCompilation()
        .AddNewtonsoftJson(jsonOptions =>
        {
            jsonOptions.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            jsonOptions.SerializerSettings.MaxDepth = 5;
            jsonOptions.SerializerSettings.ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            };
        })
        .AddJsonOptions(x => x.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull)
        .AddDataAnnotationsLocalization(
            options =>
            {
                options.DataAnnotationLocalizerProvider = (type, factory) =>
                {
                    var assemblyName = new AssemblyName(typeof(SharedResource).GetTypeInfo().Assembly.FullName!);
                    return factory.Create("SharedResource", assemblyName.Name!);
                };
            }
        )
        .AddViewLocalization();


    var settings = new SSOSettings();

    builder.Configuration.GetSection(nameof(SSOSettings)).Bind(settings);

    builder.Services.AddOptions<SSOSettings>()
        .BindConfiguration(nameof(SSOSettings))
        .Validate(config =>
        {
            if (string.IsNullOrEmpty(config.IdentityServerUrl) || config.IdentityServerUrl.Length > 150)
                return false;
            if (string.IsNullOrEmpty(config.ClientId) || config.ClientId.Length > 150)
                return false;
            if (string.IsNullOrEmpty(config.ClientSecret) || config.ClientSecret.Length > 150)
                return false;
            if (string.IsNullOrEmpty(config.AddAudience) || config.AddAudience.Length > 150)
                return false;
            return true;
        })
        .ValidateOnStart()
        ;

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(c =>
    {
        c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
        {
            Type = SecuritySchemeType.OAuth2,
            Flows = new OpenApiOAuthFlows
            {
                ClientCredentials = new OpenApiOAuthFlow
                {
                    TokenUrl = new Uri(settings.IdentityServerUrl + "/connect/token"),
                    Scopes = new Dictionary<string, string>
                    {
                        { $"{settings.AddAudience}_scope", settings.AddAudience.Humanize() }
                    },
                    AuthorizationUrl = new Uri(settings.IdentityServerUrl + "/connect/authorize")
                }
            }
        });

        c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "oauth2"
                    }
                },
                new string[] { }
            }
        });
    });

    builder.Services.AddHttpContextAccessor();


    builder.Services.AddHealthChecks()
        .AddCheck<SqlServerHealthCheck>("database")
        ;

    builder.Services.AddAuthentication(OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme);

// Register the OpenIddict validation components.
    builder.Services.AddOpenIddict()
        .AddValidation(options =>
        {
            // Note: the validation handler uses OpenID Connect discovery
            // to retrieve the address of the introspection endpoint.
            options.SetIssuer(settings.IdentityServerUrl);
            // options.SetIssuer("https://localhost:7217");
            options.AddAudiences(settings.AddAudience);

            // Configure the validation handler to use introspection and register the client
            // credentials used when communicating with the remote introspection endpoint.
            options.UseIntrospection()
                .SetClientId(settings.ClientId)
                .SetClientSecret(settings.ClientSecret);

            // Register the System.Net.Http integration.
            options.UseSystemNetHttp();

            // Register the ASP.NET Core host.
            options.UseAspNetCore();

            // options.EnableTokenEntryValidation();
            // options.EnableAuthorizationEntryValidation();
        });

    //
    // var notificationSettings = new NotificationServiceSettings();
    // builder.Configuration.GetSection(nameof(NotificationServiceSettings)).Bind(notificationSettings);
    // builder.Services.AddNotificationServices(notificationSettings);

    //
    // builder.Services.AddAuthentication(options =>
    // {
    //     options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    //     options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
    // });


    var typeAdapterConfig = TypeAdapterConfig.GlobalSettings;
    typeAdapterConfig.Compiler = exp => exp.CompileWithDebugInfo(new ExpressionCompilationOptions
        { EmitFile = true, ThrowOnFailedCompilation = true });
    typeAdapterConfig.Default.PreserveReference(true);
    typeAdapterConfig.Default.MaxDepth(5);
    typeAdapterConfig.Default.MapToConstructor(true);
    // typeAdapterConfig.EnableImmutableMapping();

    TypeAdapterConfig.GlobalSettings.Compiler = exp => exp.CompileWithDebugInfo();

    typeAdapterConfig.Scan(typeof(BaseDto<,>).Assembly);
    typeAdapterConfig.Scan(typeof(IUnitOfWork).Assembly);

    var mapperConfig = new Mapper(typeAdapterConfig);
    builder.Services.AddSingleton<IMapper>(mapperConfig);

    builder.Services.AddAuthorization();

    builder.Services.AddLazyCache();

    builder.Services.AddHealthChecks();

    builder.Services.AddTransient<RequestHandler>();

    builder.Services.AddTransient<ICorrelationIdAccessor, CorrelationIdAccessor>();

    builder.Services.Configure<ForwardedHeadersOptions>(options =>
    {
        options.ForwardedHeaders =
            ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
    });


    builder.Services.AddScoped<IViewRenderService, ViewRenderService>();
    builder.Services.AddTransient<ISearchManager, SearchManager>();
    builder.Services.AddImageflowHybridCache(
        new HybridCacheOptions(Path.Join(builder.Environment.ContentRootPath, "Storage", "Images", "cache"))
        {
            // How long after a file is created before it can be deleted
            MinAgeToDelete = TimeSpan.FromSeconds(10),
            // How much RAM to use for the write queue before switching to synchronous writes
            QueueSizeLimitInBytes = 100 * 1000 * 1000,
            // The maximum size of the cache (1GB)
            CacheSizeLimitInBytes = 1024 * 1024 * 1024,
        });

    // builder.WebHost.UseKestrel(opts => { opts.ListenAnyIP(8006); });


    builder.Services.AddProblemDetails(detailsOptions =>
    {
        detailsOptions.IncludeExceptionDetails = (e, ee) => false;
        detailsOptions.MapToStatusCode<ValidationException>(StatusCodes.Status406NotAcceptable);
        // detailsOptions.Map<Exception>(s =>
        // {
        //     if (s is FormsException formsException)
        //         return new ExceptionProblemDetails(s, StatusCodes.Status406NotAcceptable);
        //     return new ExceptionProblemDetails(s, StatusCodes.Status500InternalServerError);
        // });
    });

    // builder.Services.Configure<ApiBehaviorOptions>(options =>
    // {
    //     options.SuppressModelStateInvalidFilter = true;
    // });

    var app = builder.Build();
    var options = app.Services.GetService<IOptions<RequestLocalizationOptions>>();

    app.UseMiddleware<LogHeaderMiddleware>();
    // app.Use(async (httpContext, next) =>
    // {
    //     try
    //     {
    //         var status = httpContext.Response.StatusCode;
    //         if (status == 401 || status == 403)
    //         {
    //             var exception = httpContext.Features.Get<IExceptionHandlerFeature>();
    //             if (exception != null)
    //                 Console.WriteLine(JsonConvert.SerializeObject(exception?.Error));
    //         }
    //     }
    //     catch (Exception e)
    //     {
    //         Console.WriteLine(e);
    //     }
    //
    //     await next.Invoke();
    // });

    app.UseProblemDetails();

// Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseForwardedHeaders();
    }
    else
    {
        app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
        app.UseForwardedHeaders();
    }

    app.UseHttpsRedirection();

    app.UseStaticFiles(new StaticFileOptions
    {
        OnPrepareResponse = ctx =>
        {
            const int durationInSeconds = 60 * 60 * 24;
            ctx.Context.Response.Headers[HeaderNames.CacheControl] = "public,max-age=" + durationInSeconds;
        }
    });
    
    


    app.Use(async (httpContext, next) =>
    {
        try
        {
            var lang = "ar-EG" ;
            var cult = new CultureInfo(lang);
            cult.NumberFormat.CurrencySymbol = "SAR";
            cult.DateTimeFormat.ShortDatePattern = "dd-MM-yyyy";
            CultureInfo.DefaultThreadCurrentCulture = cult;
            CultureInfo.DefaultThreadCurrentUICulture = cult;

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    
        await next.Invoke();
    });


    app.UseRequestLocalization(options!.Value);

    app.UseRouting();
    app.UseCors("MyAllowSpecificOrigins");
    app.UseAuthentication();
    app.UseAuthorization();

    app.UseSerilogRequestLogging();


    app.MapDefaultControllerRoute().RequireAuthorization();

    app.MapHealthChecks("/health").AllowAnonymous();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Unhandled exception");
}
finally
{
    Log.Information("Shut down complete");
    Log.CloseAndFlush();
}