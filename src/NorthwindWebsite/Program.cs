using NorthwindWebsite.Configuration;
using NorthwindWebsite.Core.ApplicationSettings;
using Serilog;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

var appSettings = new AppSettings().GetAppSettings(builder.Configuration);

builder.Services.AddSingleton(appSettings);

builder.Services.AddServicesConfiguration(appSettings);

builder.Host.UseSerilog();

builder.Services.AddDbContextConfiguration(appSettings.ConnectionStrings.Default);

Log.Warning(string.Format("Application started from {0}", AppDomain.CurrentDomain.BaseDirectory));

Log.Warning(string.Format("Application configuration: {0}", JsonSerializer.Serialize(appSettings)));

var app = builder.Build();

app.AddMiddlewareConfiguration(appSettings, Log.Logger);

app.Run();
