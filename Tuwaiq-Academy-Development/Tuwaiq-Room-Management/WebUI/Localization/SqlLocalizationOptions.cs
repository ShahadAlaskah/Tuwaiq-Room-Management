// using System.Collections;
// using System.Collections.Concurrent;
// using System.Globalization;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.Extensions.DependencyInjection.Extensions;
// using Microsoft.Extensions.Localization;
// using Microsoft.Extensions.Options;
// using Infrastructure.Persistence.Ef;
//
// namespace Tuwaiq.Bootcamps.WebUI.Localization;
//
// public class SqlLocalizationOptions
// {
//     /// <summary>
//     /// If UseOnlyPropertyNames is false, this property can be used to define keys with full type names or just the name of the class
//     /// </summary>
//     public bool UseTypeFullNames { get; set; }
//
//     /// <summary>
//     /// This can be used to use only property names to find the keys
//     /// </summary>
//     public bool UseOnlyPropertyNames { get; set; }
//
//     /// <summary>
//     /// Returns only the Key if the value is not found. If set to false, the search key in the database is returned.
//     /// </summary>
//     public bool ReturnOnlyKeyIfNotFound { get; set; }
//
//     /// <summary>
//     /// Creates a new item in the SQL database if the resource is not found
//     /// </summary>
//     public bool CreateNewRecordWhenLocalisedStringDoesNotExist { get; set; }
//
//     /// <summary>
//     /// You can set the required properties to set, get, display the different localization
//     /// </summary>
//     /// <param name="useTypeFullNames"></param>
//     /// <param name="useOnlyPropertyNames"></param>
//     /// <param name="returnOnlyKeyIfNotFound"></param>
//     /// <param name="createNewRecordWhenLocalisedStringDoesNotExist"></param>
//     public void UseSettings(bool useTypeFullNames, bool useOnlyPropertyNames, bool returnOnlyKeyIfNotFound, bool createNewRecordWhenLocalisedStringDoesNotExist)
//     {
//         UseTypeFullNames = useTypeFullNames;
//         UseOnlyPropertyNames = useOnlyPropertyNames;
//         ReturnOnlyKeyIfNotFound = returnOnlyKeyIfNotFound;
//         CreateNewRecordWhenLocalisedStringDoesNotExist = createNewRecordWhenLocalisedStringDoesNotExist;
//     }
// }
//
// public static class SqlLocalizationServiceCollectionExtensions
//     {
//         /// <summary>
//         /// Adds services required for application localization.
//         /// </summary>
//         /// <param name="services">The <see cref="IServiceCollection"/> to add the services to.</param>
//         /// <returns>The <see cref="IServiceCollection"/>.</returns>
//         public static IServiceCollection AddSqlLocalization(this IServiceCollection services)
//         {
//             if (services == null)
//             {
//                 throw new ArgumentNullException(nameof(services));
//             }
//
//             return AddSqlLocalization(services, setupAction: null);
//         }
//
//         /// <summary>
//         /// Adds services required for application localization.
//         /// </summary>
//         /// <param name="services">The <see cref="IServiceCollection"/> to add the services to.</param>
//         /// <param name="setupAction">An action to configure the <see cref="LocalizationOptions"/>.</param>
//         /// <returns>The <see cref="IServiceCollection"/>.</returns>
//         public static IServiceCollection AddSqlLocalization(
//             this IServiceCollection services,
//             Action<SqlLocalizationOptions> setupAction)
//         {
//             if (services == null)
//             {
//                 throw new ArgumentNullException(nameof(services));
//             }
//
//             services.TryAdd(new ServiceDescriptor(
//                 typeof(IStringExtendedLocalizerFactory),
//                 typeof(SqlStringLocalizerFactory),
//                 ServiceLifetime.Singleton));
//             services.TryAdd(new ServiceDescriptor(
//                 typeof(IStringLocalizerFactory),
//                 typeof(SqlStringLocalizerFactory),
//                 ServiceLifetime.Singleton));
//             services.TryAdd(new ServiceDescriptor(
//                 typeof(IStringLocalizer),
//                 typeof(SqlStringLocalizer),
//                 ServiceLifetime.Transient));
//             services.TryAdd(new ServiceDescriptor(
//               typeof(DevelopmentSetup),
//               typeof(DevelopmentSetup),
//               ServiceLifetime.Singleton));
//
//             if (setupAction != null)
//             {
//                 services.Configure(setupAction);
//             }
//             return services;
//         }
//
//         public static IServiceCollection AddLocalizationSqlSchema(
//             this IServiceCollection services,
//             string schema)
//         {
//             // Adds services required for using options.
//             services.AddOptions();
//
//             // Registers the following lambda used to configure options.
//             services.Configure<SqlContextOptions>(myOptions =>
//             {
//                 myOptions.SqlSchemaName = schema;
//             });
//
//             return services;
//         }
//
//     }
//
// public interface IStringExtendedLocalizerFactory : IStringLocalizerFactory
// {
//     void ResetCache();
//
//     void ResetCache(Type resourceSource);
//
//     // IList GetImportHistory();
//     //
//     // IList GetExportHistory();
//
//     IList GetLocalizationData(string reason = "export");
//
//     IList GetLocalizationData(DateTime from, string culture = null, string reason = "export");
//
//     void UpdateLocalizationData(List<LocalizationRecord> data, string information);
//
//     void AddNewLocalizationData(List<LocalizationRecord> data, string information);
// }
//
// public class SqlStringLocalizerFactory : IStringLocalizerFactory, IStringExtendedLocalizerFactory
// {
//     private readonly DevelopmentSetup _developmentSetup;
//     private readonly LocalizationModelContext _context;
//     private static readonly ConcurrentDictionary<string, IStringLocalizer> _resourceLocalizations = new ConcurrentDictionary<string, IStringLocalizer>();
//     private readonly IOptions<SqlLocalizationOptions> _options;
//     private const string Global = "global";
//
//     public SqlStringLocalizerFactory(
//         LocalizationModelContext context,
//         DevelopmentSetup developmentSetup,
//         IOptions<SqlLocalizationOptions> localizationOptions)
//     {
//         _options = localizationOptions ?? throw new ArgumentNullException(nameof(localizationOptions));
//         _context = context ?? throw new ArgumentNullException(nameof(LocalizationModelContext));
//         _developmentSetup = developmentSetup;
//     }
//
//     public IStringLocalizer Create(Type resourceSource)
//     {
//         var returnOnlyKeyIfNotFound = _options.Value.ReturnOnlyKeyIfNotFound;
//         var createNewRecordWhenLocalisedStringDoesNotExist = _options.Value.CreateNewRecordWhenLocalisedStringDoesNotExist;
//         SqlStringLocalizer sqlStringLocalizer;
//
//         if (_options.Value.UseOnlyPropertyNames)
//         {
//             if (_resourceLocalizations.Keys.Contains(Global))
//             {
//                 return _resourceLocalizations[Global];
//             }
//
//             sqlStringLocalizer = new SqlStringLocalizer(GetAllFormDatabaseForResource(Global), _developmentSetup, Global, returnOnlyKeyIfNotFound, createNewRecordWhenLocalisedStringDoesNotExist);
//             return _resourceLocalizations.GetOrAdd(Global, sqlStringLocalizer);
//         }
//         else if (_options.Value.UseTypeFullNames)
//         {
//             if (_resourceLocalizations.Keys.Contains(resourceSource.FullName))
//             {
//                 return _resourceLocalizations[resourceSource.FullName];
//             }
//
//             sqlStringLocalizer = new SqlStringLocalizer(GetAllFormDatabaseForResource(resourceSource.FullName), _developmentSetup, resourceSource.FullName, returnOnlyKeyIfNotFound, createNewRecordWhenLocalisedStringDoesNotExist);
//             return _resourceLocalizations.GetOrAdd(resourceSource.FullName, sqlStringLocalizer);
//         }
//
//         if (_resourceLocalizations.Keys.Contains(resourceSource.Name))
//         {
//             return _resourceLocalizations[resourceSource.Name];
//         }
//
//         sqlStringLocalizer = new SqlStringLocalizer(GetAllFormDatabaseForResource(resourceSource.Name), _developmentSetup, resourceSource.Name, returnOnlyKeyIfNotFound, createNewRecordWhenLocalisedStringDoesNotExist);
//         return _resourceLocalizations.GetOrAdd(resourceSource.Name, sqlStringLocalizer);
//     }
//
//     public IStringLocalizer Create(string baseName, string location)
//     {
//         var returnOnlyKeyIfNotFound = _options.Value.ReturnOnlyKeyIfNotFound;
//         var createNewRecordWhenLocalisedStringDoesNotExist = _options.Value.CreateNewRecordWhenLocalisedStringDoesNotExist;
//
//         var resourceKey = baseName + location;
//         if (_options.Value.UseOnlyPropertyNames)
//         {
//             resourceKey = Global;
//         }
//
//         if (_resourceLocalizations.Keys.Contains(resourceKey))
//         {
//             return _resourceLocalizations[resourceKey];
//         }
//
//         var sqlStringLocalizer = new SqlStringLocalizer(GetAllFormDatabaseForResource(resourceKey), _developmentSetup, resourceKey, returnOnlyKeyIfNotFound, createNewRecordWhenLocalisedStringDoesNotExist);
//         return _resourceLocalizations.GetOrAdd(resourceKey, sqlStringLocalizer);
//     }
//
//     public void ResetCache()
//     {
//         _resourceLocalizations.Clear();
//
//         lock (_context)
//         {
//             _context.DetachAllEntities();
//         }
//     }
//
//     public void ResetCache(Type resourceSource)
//     {
//         IStringLocalizer returnValue;
//         _resourceLocalizations.TryRemove(resourceSource.FullName, out returnValue);
//
//         lock (_context)
//         {
//             _context.DetachAllEntities();
//         }
//     }
//
//     private Dictionary<string, string> GetAllFormDatabaseForResource(string resourceKey)
//     {
//         lock (_context)
//         {
//             return _context.LocalizationRecords.Where(data => data.ResourceKey == resourceKey)
//                 .ToDictionary(kvp => (kvp.Key + "." + kvp.LocalizationCulture), kvp => kvp.Text, StringComparer.OrdinalIgnoreCase);
//         }
//     }
//
//     // public IList GetImportHistory()
//     // {
//     //     lock (_context)
//     //     {
//     //         return _context.ImportHistoryDbSet.ToList();
//     //     }
//     // }
//     //
//     // public IList GetExportHistory()
//     // {
//     //     lock (_context)
//     //     {
//     //         return _context.ExportHistoryDbSet.ToList();
//     //     }
//     // }
//
//     public IList GetLocalizationData(string reason = "export")
//     {
//         lock (_context)
//         {
//             // _context.ExportHistoryDbSet.Add(new ExportHistory { Reason = reason, Exported = DateTime.UtcNow });
//             // _context.SaveChanges();
//
//             return _context.LocalizationRecords.ToList();
//         }
//     }
//
//     public IList GetLocalizationData(DateTime from, string culture = null, string reason = "export")
//     {
//         lock (_context)
//         {
//             // _context.ExportHistoryDbSet.Add(new ExportHistory { Reason = reason, Exported = DateTime.UtcNow });
//             // _context.SaveChanges();
//
//             if (culture != null)
//             {
//                 return _context.LocalizationRecords.Where(item =>
//                         EF.Property<DateTime>(item, "UpdatedTimestamp") > from &&
//                         item.LocalizationCulture == culture)
//                     .ToList();
//             }
//
//             return _context.LocalizationRecords
//                 .Where(item => EF.Property<DateTime>(item, "UpdatedTimestamp") > from).ToList();
//         }
//     }
//
//
//     public void UpdateLocalizationData(List<LocalizationRecord> data, string information)
//     {
//         lock (_context)
//         {
//             _context.DetachAllEntities();
//             _context.UpdateRange(data);
//             // _context.ImportHistoryDbSet.Add(new ImportHistory { Information = information, Imported = DateTime.UtcNow });
//             _context.SaveChanges();
//         }
//     }
//
//     public void AddNewLocalizationData(List<LocalizationRecord> data, string information)
//     {
//         lock (_context)
//         {
//             _context.DetachAllEntities();
//             _context.AddRange(data);
//             // _context.ImportHistoryDbSet.Add(new ImportHistory { Information = information, Imported = DateTime.UtcNow });
//             _context.SaveChanges();
//         }
//     }
// }
//
// public class DevelopmentSetup
// {
//     private readonly LocalizationModelContext _context;
//     private readonly IOptions<SqlLocalizationOptions> _options;
//     private readonly IOptions<RequestLocalizationOptions> _requestLocalizationOptions;
//
//     public DevelopmentSetup(
//         LocalizationModelContext context,
//         IOptions<SqlLocalizationOptions> localizationOptions,
//         IOptions<RequestLocalizationOptions> requestLocalizationOptions)
//     {
//         _options = localizationOptions;
//         _context = context;
//         _requestLocalizationOptions = requestLocalizationOptions;
//     }
//
//     public void AddNewLocalizedItem(string key, string culture, string resourceKey)
//     {
//         if (_requestLocalizationOptions.Value.SupportedCultures!.Contains(new System.Globalization.CultureInfo(culture)))
//         {
//             string computedKey = $"{key}.{culture}";
//
//             LocalizationRecord localizationRecord = new LocalizationRecord()
//             {
//                 LocalizationCulture = culture,
//                 Key = key,
//                 Text = computedKey,
//                 ResourceKey = resourceKey
//             };
//
//             lock (_context)
//             {
//                 _context.LocalizationRecords?.Add(localizationRecord);
//                 _context.SaveChanges();
//             }
//         }
//     }
// }
//
// public class SqlStringLocalizer : IStringLocalizer
// {
//     private readonly Dictionary<string, string> _localizations;
//
//     private readonly DevelopmentSetup _developmentSetup;
//     private readonly string _resourceKey;
//     private bool _returnKeyOnlyIfNotFound;
//     private bool _createNewRecordWhenLocalisedStringDoesNotExist;
//
//     public SqlStringLocalizer(Dictionary<string, string> localizations, DevelopmentSetup developmentSetup, string resourceKey, bool returnKeyOnlyIfNotFound, bool createNewRecordWhenLocalisedStringDoesNotExist)
//     {
//         _localizations = localizations;
//         _developmentSetup = developmentSetup;
//         _resourceKey = resourceKey;
//         _returnKeyOnlyIfNotFound = returnKeyOnlyIfNotFound;
//         _createNewRecordWhenLocalisedStringDoesNotExist = createNewRecordWhenLocalisedStringDoesNotExist;
//     }
//
//     public LocalizedString this[string name]
//     {
//         get
//         {
//             bool notSucceed;
//             var text = GetText(name, out notSucceed);
//
//             return new LocalizedString(name, text, notSucceed);
//         }
//     }
//
//     public LocalizedString this[string name, params object[] arguments]
//     {
//         get
//         {
//             var localizedString = this[name];
//             return new LocalizedString(name, String.Format(localizedString.Value, arguments), localizedString.ResourceNotFound);
//         }
//     }
//
//     public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
//     {
//         throw new NotImplementedException();
//     }
//
//     public IStringLocalizer WithCulture(CultureInfo culture)
//     {
//         throw new NotImplementedException();
//     }
//
//     private string GetText(string key, out bool notSucceed)
//     {
// #if NET451
//             var culture = System.Threading.Thread.CurrentThread.CurrentCulture;
// #elif NET46
//             var culture = System.Threading.Thread.CurrentThread.CurrentCulture;
// #else
//         var culture = CultureInfo.CurrentCulture;
// #endif
//
//         string computedKey = $"{key}.{culture}";
//         string parentComputedKey = $"{key}.{culture.Parent.TwoLetterISOLanguageName}";
//
//         string result;
//         if (_localizations.TryGetValue(computedKey, out result) || _localizations.TryGetValue(parentComputedKey, out result))
//         {
//             notSucceed = false;
//             return result;
//         }
//         else
//         {
//             notSucceed = true;
//             if (_createNewRecordWhenLocalisedStringDoesNotExist)
//             {
//                 _developmentSetup.AddNewLocalizedItem(key, culture.ToString(), _resourceKey);
//                 _localizations.Add(computedKey, computedKey);
//                 return computedKey;
//             }
//
//             if (_returnKeyOnlyIfNotFound)
//             {
//                 return key;
//             }
//
//             return _resourceKey + "." + computedKey;
//         }
//     }
// }
