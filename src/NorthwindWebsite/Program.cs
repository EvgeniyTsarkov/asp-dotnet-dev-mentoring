using NorthwindWebsite.Configuration;
using NorthwindWebsite.Core.ApplicationSettings;

var builder = WebApplication.CreateBuilder(args);

var appSettings = new AppSettings().GetAppSettings(builder.Configuration);

builder.Services.AddSingleton(appSettings);

builder.Services.AddServicesConfiguration(builder.Configuration);

builder.Services.AddDbContextConfiguration(appSettings.ConnectionStrings.Default);

var app = builder.Build();

app.AddMiddlewareConfiguration(builder.Configuration);

app.Run();