using BilgeSinema.Mvc.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

var settings = builder.Configuration.GetSection("AppSettings").Get<AppSettings>();

builder.Services.AddSingleton(settings);

var app = builder.Build();


 app.MapDefaultControllerRoute();

app.Run();
