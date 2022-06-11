using Microsoft.AspNetCore.Mvc.Filters;
using NorthwindWebsite.Core.ApplicationSettings;

namespace NorthwindWebsite.Filters;

public class LoggingFilter : IActionFilter
{
    private readonly ILogger<LoggingFilter> _logger;
    private readonly AppSettings _appSettings;
    private readonly bool _isLoggingEnabled;

    public LoggingFilter(
        ILogger<LoggingFilter> logger,
        AppSettings appSettings)
    {
        _logger = logger;
        _appSettings = appSettings;
        _isLoggingEnabled = appSettings.SerilogConfiguration.ActionsLoggingEnabled;
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (!_isLoggingEnabled)
        {
            return;
        }

        _logger.LogWarning(string.Format("Action '{0}' started...", context.ActionDescriptor.DisplayName));
        _logger.LogWarning(string.Format("Method: {0}", context.HttpContext.Request.Method));
        _logger.LogWarning(string.Format("Path: {0}", context.HttpContext.Request.Path));
        _logger.LogWarning("Request headers:");

        LogHeaders(context.HttpContext.Request.Headers);
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        if (!_isLoggingEnabled)
        {
            return;
        }

        _logger.LogWarning(string.Format("Action '{0}' finished...", context.ActionDescriptor.DisplayName));
        _logger.LogWarning(string.Format("Status code: {0}", context.HttpContext.Response.StatusCode));
        _logger.LogWarning(string.Format("Response headers:"));

        LogHeaders(context.HttpContext.Response.Headers);

        var containsBody = context.HttpContext.Response.Body.CanRead;

        _logger.LogWarning(string.Format("Contains body: {0}", containsBody));
    }

    private void LogHeaders(IHeaderDictionary headers) =>
        headers.ToList().ForEach(header =>
                _logger.LogWarning($"{header.Key}: {header.Value}"));
}
