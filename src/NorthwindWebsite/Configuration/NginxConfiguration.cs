using Microsoft.AspNetCore.HttpOverrides;

namespace NorthwindWebsite.Configuration;

public static class NginxConfiguration
{
    public static void ConfigureNginxServer(this WebApplication app) =>
        app.UseForwardedHeaders(new ForwardedHeadersOptions
        {
            ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
        });
}
