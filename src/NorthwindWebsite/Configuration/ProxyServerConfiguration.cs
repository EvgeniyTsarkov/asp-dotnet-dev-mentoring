using Microsoft.AspNetCore.HttpOverrides;

namespace NorthwindWebsite.Configuration;

public static class ProxyServerConfiguration
{
    public static void ConfigureProxyServer(this WebApplication app) =>
        app.UseForwardedHeaders(new ForwardedHeadersOptions
        {
            ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
        });
}
