using System.Globalization;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using BT.Logger.Abstraction;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using OpenIddict.Abstractions;
using WebUI.Settings;
using WebUI.Swagger;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.DataProtection;
using OpenIddict.Client;
using SDK;
using Serilog;
using WebUI.Extensions;
using WebUI.Models;


try
{
    var supportedCultures = new[] {new CultureInfo("ar-EG") };

    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");


    var ssoSettings = new SSOSettings();

    var logsConnectionString = builder.Configuration.GetConnectionString("LogsDbConnection");

    builder.Services.AddLoggerServices(builder.Configuration, logsConnectionString!, builder.Host, "Logs");

    builder.Configuration.GetSection(nameof(SSOSettings)).Bind(ssoSettings);

    builder.Services.AddOptions<SSOSettings>()
        .BindConfiguration(nameof(SSOSettings))
        .ValidateOnStart()
        ;

    var formTemplateOptions = new FormTemplateOptions();
    builder.Configuration.GetSection(nameof(FormTemplateOptions)).Bind(formTemplateOptions);
    builder.Services.AddOptions<FormTemplateOptions>().BindConfiguration(nameof(FormTemplateOptions)).ValidateOnStart();
    builder.Services.AddSingleton<FormTemplateOptions>(formTemplateOptions);

    builder.Services.Configure<RequestLocalizationOptions>(options =>
    {
        var cultureInfo = supportedCultures.First();
        options.DefaultRequestCulture = new RequestCulture(cultureInfo);
        var optionsSupportedCultures = supportedCultures.Select(s => s).ToList();
        options.SupportedCultures = optionsSupportedCultures;
        options.SupportedUICultures = optionsSupportedCultures;
    });

// // builder.Services.AddLiveReload(config =>
// {
//     // optional - use config instead
//     //config.LiveReloadEnabled = true;
//     //config.FolderToMonitor = Path.GetFullname(Path.Combine(Env.ContentRootPath,"..")) ;
// });
    builder.Services.AddRazorPages().AddRazorRuntimeCompilation().AddNewtonsoftJson(jsonOptions =>
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
                    var assemblyName = new AssemblyName(Assembly.GetExecutingAssembly().FullName!);
                    return factory.Create("SharedResource", assemblyName.Name!);
                };
            }
        ).AddViewLocalization(
            LanguageViewLocationExpanderFormat.Suffix,
            localizationOptions => { localizationOptions.ResourcesPath = "Resources"; }
        );

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
        .AddJsonOptions(x =>
        {
            x.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            x.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            x.JsonSerializerOptions.WriteIndented = true;
        })
        .AddViewLocalization()
        ;

    builder.Services.AddScoped<LanguageActionFilter>();

    builder.Services.Configure<RequestLocalizationOptions>(
        options =>
        {
            var cult = new RequestCulture("ar-EG", "ar-EG");
            cult.Culture.NumberFormat.CurrencySymbol = "SAR";
            cult.Culture.DateTimeFormat.ShortDatePattern = "dd-MM-yyyy";

            options.DefaultRequestCulture = cult;
            options.SupportedCultures = supportedCultures;
            options.SupportedUICultures = supportedCultures;
            options.RequestCultureProviders = new[]
            {
                new CookieRequestCultureProvider
                {
                    /*CookieName = "BadawiTECH.Edu.Admin.Culture"*/
                }
            };
        });

    builder.Services.AddScoped<ValidateMimeMultipartContentFilter>();

    builder.Services.AddCors(options =>
    {
        options.AddPolicy("MyAllowSpecificOrigins",
            policy =>
            {
                var origins = builder.Configuration.GetValue<string>("AllowedHosts")?.Split(';');
                if (origins != null)
                    policy.WithOrigins(origins).AllowAnyHeader()
                        .AllowAnyMethod().WithExposedHeaders("Content-Disposition");
            });
    });

    builder.Services.AddSignalR();

    builder.Services.AddLazyCache();

//
// if (builder.Environment.IsProduction())
// {
//     builder.WebHost.UseKestrel(opts =>
//     {
//         opts.ListenAnyIP(8300);
//
//         if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX) || RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
//         {
//             opts.ConfigureHttpsDefaults(adapterOptions =>
//             {
//                 var pass = "P@ssw0rd@Tuwaiq@007";
//                 adapterOptions.ServerCertificate = new X509Certificate2("tuwaiqdev-sign.pfx", pass,
//                     X509KeyStorageFlags.MachineKeySet);
//             });
//         }
//         else
//         {
//             opts.ConfigureHttpsDefaults(adapterOptions =>
//             {
//                 var pass = "P@ssw0rd@Tuwaiq@007";
//                 adapterOptions.ServerCertificate = new X509Certificate2("tuwaiqdev-sign.pfx", pass,
//                     X509KeyStorageFlags.MachineKeySet | X509KeyStorageFlags.Exportable);
//             });
//         }
//
//         opts.ListenAnyIP(8301, opts => { opts.UseHttps(); });
//     });
// }

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddSwaggerGen(option =>
        {
            option.OperationFilter<ReApplyOptionalRouteParameterOperationFilter>();
            option.SwaggerDoc("v1", new OpenApiInfo { Title = "Tuwaiq Portal API", Version = "v1" });
            // option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            // {
            //     In = ParameterLocation.Header,
            //     Description = "Please enter a valid token",
            //     Name = "Authorization",
            //     Type = SecuritySchemeType.Http,
            //     BearerFormat = "JWT",
            //     Scheme = "Bearer"
            //})
        })
        ;

    JsonConvert.DefaultSettings =
        () => new JsonSerializerSettings()
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            Converters = { new StringEnumConverter() }
        };

    builder.Services.AddHttpContextAccessor();

    builder.Services
        .AddDataProtection()
        .SetApplicationName("RoomManagementWebUI")
        .SetDefaultKeyLifetime(TimeSpan.FromDays(14))
        .PersistKeysToFileSystem(new DirectoryInfo(Path.Join(Environment.CurrentDirectory, "keys")))
        ;

    // var tuwaiqFormsApiSettings = new TuwaiqFormsApiSettings();
    // builder.Configuration.GetSection(nameof(TuwaiqFormsApiSettings)).Bind(tuwaiqFormsApiSettings);
    // builder.Services.AddOptions<TuwaiqFormsApiSettings>()
    //     .BindConfiguration(nameof(TuwaiqFormsApiSettings))
    //     .ValidateOnStart()
    //     ;
    //
    builder.Services.SetupFormsApi(builder.Configuration.GetValue<string>("TuwaiqRoomManagementApiUrl")!);

    // builder.Services.AddAuthentication(options =>
    //     {
    //         options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    //         options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
    //     })
    //     .AddCookie(options =>
    //     {
    //         options.LoginPath = "/signin";
    //         options.LogoutPath = "/signout";
    //         options.ExpireTimeSpan = TimeSpan.FromMinutes(19);
    //     })
    //     .AddCookie("OpenIddict.Server.AspNetCore")

    IdentityModelEventSource.ShowPII = true;

    // builder.Services.Configure<CookiePolicyOptions>(options =>
    // {
    //     options.Secure = CookieSecurePolicy.Always;
    // });

    builder.Services.AddAntiforgery(options =>
    {
        options.Cookie.Name = "my-x-12s3";
        options.HeaderName = "my-x-12s3";
    });
    
    builder.Services.AddAccessTokenManagement();
    builder.Services.AddAuthentication(options =>
        {
            options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
        })
        .AddCookie(options =>
        {
            // options.Cookie.Path = "/";
            options.LoginPath = "/signin";
            options.LogoutPath = "/signout";
            options.Events.OnSigningOut = async e =>
            {
                // revoke refresh token on sign-out
                await e.HttpContext.RevokeUserRefreshTokenAsync();
            };
            options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            options.Cookie.SameSite = SameSiteMode.None;

            options.ExpireTimeSpan = TimeSpan.FromHours(8);
            options.Cookie.Name = "TuwaiqForms";
            // options.SlidingExpiration = false;
        })
    //     ;
    //
    // builder.Services.AddOpenIddict()
    //         .AddClient(options =>
    //         {
    //             // Note: this sample uses the code flow, but you can enable the other flows if necessary.
    //             options.AllowAuthorizationCodeFlow();
    //
    //             options.AddDevelopmentEncryptionCertificate()
    //                    .AddDevelopmentSigningCertificate();
    //
    //             // Register the ASP.NET Core host and configure the ASP.NET Core-specific options.
    //             options.UseAspNetCore()
    //                    .EnableStatusCodePagesIntegration()
    //                    .EnableRedirectionEndpointPassthrough()
    //                    .EnablePostLogoutRedirectionEndpointPassthrough();
    //
    //             
    //             options.AddRegistration(new OpenIddictClientRegistration
    //             {
    //                 Issuer = new Uri(ssoSettings.IdentityServerUrl, UriKind.Absolute),
    //
    //                 ClientId = ssoSettings.ClientId,
    //                 ClientSecret = ssoSettings.ClientSecret,
    //                 Scopes =
    //                 {
    //                     OpenIddictConstants.Permissions.Scopes.Email, 
    //                     OpenIddictConstants.Permissions.Scopes.Profile,
    //                     OpenIddictConstants.Permissions.Scopes.Roles
    //                 },
    //
    //                 RedirectUri = new Uri("callback/login/local", UriKind.Relative),
    //                 PostLogoutRedirectUri = new Uri("callback/logout/local", UriKind.Relative)
    //             });
    //         });
        .AddOpenIdConnect(options =>
        {
            options.Authority = ssoSettings.IdentityServerUrl;
            options.ClientId = ssoSettings.ClientId;
            options.ClientSecret = ssoSettings.ClientSecret;
            options.ResponseType = OpenIdConnectResponseType.Code;
            options.SaveTokens = true;
            options.Scope.Add("openid");
            options.Scope.Add("profile");
            options.Scope.Add("email");
            options.Scope.Add("roles");
            if (ssoSettings.Scopes.Any())
            {
                ssoSettings.Scopes.ToList().ForEach(s => options.Scope.Add(s));
            }
        
            // options.CallbackPath = "/signin-oidc";
            options.UsePkce = true;
            // options.RequireHttpsMetadata = false;
        
            // options.MapInboundClaims = true;
            options.RequireHttpsMetadata = false;
        
            // options.SignedOutCallbackPath = "/signout-callback-oidc";
            // options.RemoteSignOutPath = "/signout-oidc";
            // options.MapInboundClaims = true;
            options.GetClaimsFromUserInfoEndpoint = true;
            // options.ClaimActions.MapUniqueJsonKey("name",
            //     "name");
            options.ClaimActions.MapUniqueJsonKey(OpenIddictConstants.Claims.Gender, OpenIddictConstants.Claims.Gender);
            options.ClaimActions.MapUniqueJsonKey("container_id", "container_id");
            options.ClaimActions.MapUniqueJsonKey("container_name", "container_name");
            options.ClaimActions.MapUniqueJsonKey(OpenIddictConstants.Claims.Name, OpenIddictConstants.Claims.Name);
            options.ClaimActions.MapUniqueJsonKey(OpenIddictConstants.Claims.MiddleName, OpenIddictConstants.Claims.MiddleName);
            options.ClaimActions.MapUniqueJsonKey(OpenIddictConstants.Claims.FamilyName, OpenIddictConstants.Claims.FamilyName);
            options.ClaimActions.MapUniqueJsonKey(OpenIddictConstants.Claims.Birthdate, OpenIddictConstants.Claims.Birthdate);
            options.ClaimActions.MapUniqueJsonKey(OpenIddictConstants.Claims.Email, OpenIddictConstants.Claims.Email);
            options.ClaimActions.MapUniqueJsonKey(OpenIddictConstants.Claims.EmailVerified,
                OpenIddictConstants.Claims.EmailVerified);
            options.ClaimActions.MapUniqueJsonKey(OpenIddictConstants.Claims.PhoneNumber, OpenIddictConstants.Claims.PhoneNumber);
            options.ClaimActions.MapUniqueJsonKey(OpenIddictConstants.Claims.PhoneNumberVerified,
                OpenIddictConstants.Claims.PhoneNumberVerified);
            options.ClaimActions.MapUniqueJsonKey(OpenIddictConstants.Claims.Profile, OpenIddictConstants.Claims.Profile);
            options.ClaimActions.MapUniqueJsonKey("isMinor", "isMinor");
            options.TokenValidationParameters = new TokenValidationParameters
            {
                NameClaimType = "name",
                // RoleClaimType = "role",
                //, RoleClaimType = "role"
            };
        });

    builder.Services.Configure<GzipCompressionProviderOptions>(options =>
        options.Level = System.IO.Compression.CompressionLevel.Fastest);

    builder.Services.AddResponseCompression(options => { options.EnableForHttps = true; });

    builder.Services.AddProblemDetails(detailsOptions => { detailsOptions.IncludeExceptionDetails = (_, _) => true; });


    var app = builder.Build();
    var options = app.Services.GetService<IOptions<RequestLocalizationOptions>>();
    

    app.UseResponseCompression();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }


    // app.ConfigureExceptionHandler();

    app.UseRequestLocalization(options!.Value);

// IMPORTANT: Before **any other output generating middleware** handlers including error handlers
// app.UseLiveReload();

    app.UseStaticFiles(new StaticFileOptions
    {
        HttpsCompression = Microsoft.AspNetCore.Http.Features.HttpsCompressionMode.Compress,

        OnPrepareResponse = ctx =>
        {
            var headers = ctx.Context.Response.GetTypedHeaders();
            headers.CacheControl = new Microsoft.Net.Http.Headers.CacheControlHeaderValue
            {
                Public = true,
                MaxAge = TimeSpan.FromDays(30)
            };
        }
    });


    app.UseSerilogRequestLogging();
    app.UseRouting();
    // app.UseHttpsRedirection();
    app.UseCors("MyAllowSpecificOrigins");

    app.UseCookiePolicy(
        new CookiePolicyOptions
        {
            Secure = CookieSecurePolicy.Always,
            MinimumSameSitePolicy = SameSiteMode.None
        }
    );
    
    app.UseAuthentication();
    app.UseAuthorization();

    app.UseProblemDetails();

    app.Use(async (httpContext, next) =>
    {
        try
        {
            // httpContext.Request.Headers.TryGetValue("s-language", out var langEnum);

            // if (!string.IsNullOrEmpty(langEnum))
            {
                var lang = "ar-EG";
                var cult = new CultureInfo(lang);
                cult.NumberFormat.CurrencySymbol = "SAR";
                cult.DateTimeFormat.ShortDatePattern = "dd-MM-yyyy";
                CultureInfo.DefaultThreadCurrentCulture = cult;
                CultureInfo.DefaultThreadCurrentUICulture = cult;
            }
        }
        catch
        {
            //
        }

        await next.Invoke();
    });

    
    app.MapDefaultControllerRoute().RequireAuthorization();

    app.MapRazorPages().RequireAuthorization();


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