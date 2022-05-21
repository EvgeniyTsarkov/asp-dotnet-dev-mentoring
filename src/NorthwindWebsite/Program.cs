using NorthwindWebsite.Configuration;
using NorthwindWebsite.Core.Application_Settings;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddServicesConfiguration(builder.Configuration);

var appSettings = new AppSettings().ReadAppSettings(builder.Configuration);

builder.Services.AddDbContextConfiguration(appSettings.ConnectionStrings.Default);

var app = builder.Build();

app.AddMiddlewareConfiguration(builder.Configuration);

app.Run();