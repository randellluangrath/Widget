using Microsoft.Extensions.Options;
using Widget.Contracts.Interfaces;
using Widget.Contracts.Models;
using Widget.Web.Clients;

namespace Widget.Web.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddWeatherApiClient(this IServiceCollection services, IConfiguration configuration)
    {
        var apiUrl = configuration["ApplicationOptions:WeatherApiUrl"]?.Trim('/') + '/';
        services.AddHttpClient<IWeatherApiClient>(httpClient =>
            {
                httpClient.BaseAddress = new Uri(apiUrl);
                httpClient.Timeout = TimeSpan.FromSeconds(30);
            },
            (httpClient, serviceProvider) => new WeatherApiClient(httpClient,
                serviceProvider.GetRequiredService<IOptions<ApplicationOptions>>(),
                serviceProvider.GetRequiredService<IWidgetCache<WeatherResponse?, string>>(),
                serviceProvider.GetRequiredService<ILogger<WeatherApiClient>>()));
    }

    private static void AddHttpClient<TClient>(this IServiceCollection services,
        Action<HttpClient> configureHttpClient,
        Func<HttpClient, IServiceProvider, TClient> configureClient) where TClient : class
    {
        var clientName = typeof(TClient).Name;

        services.AddHttpClient(clientName, configureHttpClient);

        services.AddTransient<TClient>(serviceProvider =>
        {
            var clientFactory = serviceProvider.GetRequiredService<IHttpClientFactory>();
            var httpClient = clientFactory.CreateClient(clientName);
            return configureClient(httpClient, serviceProvider);
        });
    }
}