using Microsoft.AspNetCore.Diagnostics;

namespace NorthwindWebsite.Configuration;

public static class ExceptionHandlingConfiguration
{
    public static void ConfigureErrorHandling(this WebApplication app, Serilog.ILogger logger)
    {
        app.UseExceptionHandler(appError =>
        {
            appError.Run(async context =>
            {
                var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                if (contextFeature != null)
                {
                    logger.Error($"An error occured: {contextFeature.Error}");

                    await context.Response.WriteAsync($@"
                            {{
                                ""errors"": [
                                    ""status"": ""{context.Response.StatusCode}"",
                                    ""message"":""{contextFeature.Error.Message}"",
                                    ""Address the logs for additional information""
                                ]
                            }}
                        ");
                }
            });
        });
    }
}
