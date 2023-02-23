using Widget.Contracts.Interfaces;
using Widget.Contracts.Models;
using Widget.Contracts.Wrappers;
using Widget.Infrastructure.Repositories;
using Widget.Web.Cache;
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
builder.Services.AddSingleton<IWidgetCache<WeatherResponse, string>, WidgetCache<WeatherResponse, string>>();
builder.Services.AddWeatherApiClient(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder => { builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin(); });
});


var app = builder.Build();

app.UseAuthentication()
    .UseCors(opt => opt.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

if (app.Environment.IsDevelopment())
{
    // Add swagger.
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    });
}

app.UseHttpsRedirection();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");



app.Run();