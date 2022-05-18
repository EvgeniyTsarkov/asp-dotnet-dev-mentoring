using NorthwindWebsite.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContextConfiguration("Server=(localdb)\\mssqllocaldb; Database=Northwind; Trusted_Connection=True;");
builder.Services.AddServicesConfiguration(builder.Configuration);

var app = builder.Build();

app.AddMiddlewareConfiguration(builder.Configuration);

app.Run();
