using NorthwindWebsite.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContextConfiguration(builder.Configuration.GetConnectionString("Default"));
builder.Services.AddServicesConfiguration(builder.Configuration);

var app = builder.Build();

app.AddMiddlewareConfiguration(builder.Configuration);

app.Run();
