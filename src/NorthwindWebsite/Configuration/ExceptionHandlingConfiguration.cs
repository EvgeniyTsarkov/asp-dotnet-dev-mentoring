using Microsoft.AspNetCore.Diagnostics;
using Newtonsoft.Json;
using NorthwindWebsite.Business.Models;
using Serilog;

namespace NorthwindWebsite.Configuration;

public static class ExceptionHandlingConfiguration
{
    public static void ConfigureErrorHandling(this WebApplication app)
    {
        app.UseExceptionHandler(appError =>
        {
            appError.Run(async context =>
            {
                var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                if (contextFeature != null)
                {
                    Log.Error($"An error occured: {contextFeature.Error.Message}");

                    var errorData = new ErrorDetails
                    {
                        StatusCode = context.Response.StatusCode,
                        Message = contextFeature.Error.Message
                    };

                    await context.Response.WriteAsync(JsonConvert.SerializeObject(errorData));
                }
            });
        });
    }
}
