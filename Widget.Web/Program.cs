using Widget.Contracts.Interfaces;
using Widget.Contracts.Models;
using Widget.Contracts.Wrappers;
using Widget.Infrastructure.Repositories;
using Widget.Web.Clients;
using Widget.Web.Extensions;
using Widget.Web.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<ApplicationOptions>(builder.Configuration.GetSection(key: nameof(ApplicationOptions)));
builder.Services.AddScoped<INewsApiClientWrapper, NewsApiClientWrapper>();
builder.Services.AddScoped<IWeatherApiClient, WeatherApiClient>();
builder.Services.AddScoped<ILocalService, LocalService>();
builder.Services.AddSingleton<ILocalRepository, LocalRepository>();
builder.Services.AddWeatherApiClient(builder.Configuration);
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");

app.Run();