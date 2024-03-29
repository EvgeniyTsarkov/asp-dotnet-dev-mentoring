using NorthwindWebsite.Configuration;
using NorthwindWebsite.Core.ApplicationSettings;
using NorthwindWebsite.Core.EmailSender;
using Serilog;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables();

var appSettings = new AppSettings().GetAppSettings(builder.Configuration);

builder.Services.AddSingleton(appSettings);

builder.Services.AddServicesConfiguration(appSettings);

builder.Services.Configure<AuthMessageSenderOptions>(builder.Configuration);

builder.ConfigureLogger(appSettings);

var app = builder.Build();

Log.Information(string.Format("Application started from {0}", AppDomain.CurrentDomain.BaseDirectory));

Log.Information(string.Format("Application configuration: {0}", JsonSerializer.Serialize(appSettings)));

app.AddMiddlewareConfiguration(appSettings);

app.Run();
