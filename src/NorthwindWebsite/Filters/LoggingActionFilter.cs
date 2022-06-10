using Microsoft.AspNetCore.Mvc.Filters;
using NorthwindWebsite.Core.ApplicationSettings;

namespace NorthwindWebsite.Filters;

public class LoggingActionFilter : IActionFilter
{
    private readonly ILogger<LoggingActionFilter> _logger;
    private readonly AppSettings _appSettings;
    private readonly bool _loggingEnabled;

    public LoggingActionFilter(
        ILogger<LoggingActionFilter> logger,
        AppSettings appSettings)
    {
        _logger = logger;
        _appSettings = appSettings;
        _loggingEnabled = appSettings.SerilogConfiguration.ActionsLogging;
    }
    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (_loggingEnabled)
        {
            _logger.LogWarning(string.Format("Action '{0}' started...", context.ActionDescriptor.DisplayName));
        }
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        if (_loggingEnabled)
        {
            _logger.LogWarning(string.Format("Action '{0}' finished...", context.ActionDescriptor.DisplayName));
        }
    }
}
