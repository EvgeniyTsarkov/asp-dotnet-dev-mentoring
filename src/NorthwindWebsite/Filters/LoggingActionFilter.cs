using Microsoft.AspNetCore.Mvc.Filters;
using NorthwindWebsite.Core.ApplicationSettings;
using System.Text;

namespace NorthwindWebsite.Filters;

public class LoggingFilter : IActionFilter
{
    private readonly ILogger<LoggingFilter> _logger;
    private readonly bool _isLoggingEnabled;

    public LoggingFilter(
        ILogger<LoggingFilter> logger,
        AppSettings appSettings)
    {
        _logger = logger;
        _isLoggingEnabled = appSettings.SerilogConfiguration.ActionsLoggingEnabled;
    }

    public async void OnActionExecuting(ActionExecutingContext context)
    {
        if (!_isLoggingEnabled)
        {
            return;
        }

        _logger.LogInformation(string.Format("Action '{0}' started...", context.ActionDescriptor.DisplayName));
        _logger.LogInformation(string.Format("Method: {0}", context.HttpContext.Request.Method));
        _logger.LogInformation(string.Format("Path: {0}", context.HttpContext.Request.Path));
        _logger.LogInformation("Request headers:");

        LogHeaders(context.HttpContext.Request.Headers, "Request");

        var containsBody = context.HttpContext.Request.ContentLength != null;

        if (!containsBody)
        {
            return;
        }

        _logger.LogInformation(string.Format("Contains body: {0}", containsBody));

        var requestBody = await RequestBodyToString(context.HttpContext);

        _logger.LogInformation(string.Format("RequestBody: {0}", requestBody));
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        if (!_isLoggingEnabled)
        {
            return;
        }

        _logger.LogInformation(string.Format("Action '{0}' finished...", context.ActionDescriptor.DisplayName));
        _logger.LogInformation(string.Format("Status code: {0}", context.HttpContext.Response.StatusCode));
        _logger.LogInformation(string.Format("Response headers:"));

        LogHeaders(context.HttpContext.Response.Headers, "Response");
    }

    private void LogHeaders(IHeaderDictionary headers, string contextType)
    {
        _logger.LogInformation($"{contextType} headers:");

        headers.ToList().ForEach(header =>
                _logger.LogInformation($"{header.Key}: {header.Value}"));
    }

    private static async Task<string> RequestBodyToString(HttpContext context)
    {
        var body = string.Empty;

        context.Request.Body.Position = 0;

        using (var bodyReader = new StreamReader(context.Request.Body, Encoding.ASCII, true, 1024, leaveOpen: true))
        {
            body = await bodyReader.ReadToEndAsync();
        };

        context.Request.Body.Position = 0;

        return body;
    }
}
