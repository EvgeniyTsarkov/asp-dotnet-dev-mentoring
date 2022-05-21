using NorthwindWebsite.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationSettings(builder.Configuration);

builder.Services.AddServicesConfiguration(builder.Configuration);

builder.Services.AddDbContextConfiguration(builder.Configuration.GetConnectionString("Default"));

var app = builder.Build();

app.AddMiddlewareConfiguration(builder.Configuration);

app.Run();