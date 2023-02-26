using System.Net.Http.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Widget.Contracts.Interfaces;
using Widget.Contracts.Models;

namespace Widget.Infrastructure.Clients;

public class WeatherApiClient : IWeatherApiClient
{
    private readonly HttpClient _httpClient;
    private readonly ApplicationOptions _applicationOptions;
    private readonly IWidgetCache<WeatherResponse?, string> _widgetCache;
    private readonly ILogger<WeatherApiClient> _logger;

    private const string ResourcePath = "current.json";

    public WeatherApiClient(HttpClient httpClient, IOptions<ApplicationOptions> applicationOptions,
        IWidgetCache<WeatherResponse?, string> widgetCache, ILogger<WeatherApiClient> logger)
    {
        _httpClient = httpClient;
        _applicationOptions = applicationOptions.Value;
        _logger = logger;
        _widgetCache = widgetCache;
    }

    public async Task<WeatherResponse?> GetAsync(string? q)
    {
        var queryParameters = new Dictionary<string, string>
        {
            { "key", _applicationOptions.WeatherApiKey },
            { "q", q },
            { "aqi", "no" }
        };

        var dictFormUrlEncoded = new FormUrlEncodedContent(queryParameters);
        var queryString = await dictFormUrlEncoded.ReadAsStringAsync();

        return await _widgetCache.GetOrCreateAsync(q,
            () => GetWeatherAsync(queryString));
    }
    
    private async Task<WeatherResponse?> GetWeatherAsync(string queryString)
    {
        var response =
            await _httpClient.GetAsync($"v1/{ResourcePath}?{queryString}");

        if (response.IsSuccessStatusCode)
            return await response.Content.ReadFromJsonAsync<WeatherResponse?>();
        
        _logger.LogError($"Error calling {nameof(GetAsync)} from Weather Api Client: {response?.Content}");
        throw new HttpRequestException();
    }
}