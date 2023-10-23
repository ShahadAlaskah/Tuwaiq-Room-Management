using System.Globalization;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebUI.Extensions;

public class LanguageActionFilter : ActionFilterAttribute
{
    private readonly ILogger _logger;

    public LanguageActionFilter(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger("LanguageActionFilter");
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {         
        string culture = context.RouteData.Values["culture"].ToString();
        _logger.LogInformation($"Setting the culture from the URL: {culture}");

        CultureInfo.CurrentCulture = new CultureInfo(culture);
        CultureInfo.CurrentUICulture = new CultureInfo(culture);

        base.OnActionExecuting(context);
    }
}