using NorthwindWebsite.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddServicesConfiguration(builder.Configuration);

var app = builder.Build();

app.AddMiddlewareConfiguration(builder.Configuration);

app.Run();
