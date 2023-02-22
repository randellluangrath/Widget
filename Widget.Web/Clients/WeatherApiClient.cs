using Microsoft.Extensions.Options;
using Widget.Contracts.Interfaces;
using Widget.Contracts.Models;

namespace Widget.Web.Clients;

public class WeatherApiClient : IWeatherApiClient
{
    private readonly HttpClient _httpClient;
    private readonly ApplicationOptions _applicationOptions;
    private readonly ILogger<WeatherApiClient> _logger;

    private const string ResourcePath = "current.json";

    public WeatherApiClient(HttpClient httpClient, IOptions<ApplicationOptions> applicationOptions,
        ILogger<WeatherApiClient> logger)
    {
        _httpClient = httpClient;
        _applicationOptions = applicationOptions.Value;
        _logger = logger;
    }

    public async Task<WeatherResponse?> GetAsync(string? q)
    {
        WeatherResponse? weatherResponse;

        var queryParameters = new Dictionary<string, string>
        {
            { "key", _applicationOptions.WeatherApiKey },
            { "q", q },
            { "aqi", "no" }
        };

        var dictFormUrlEncoded = new FormUrlEncodedContent(queryParameters);
        var queryString = await dictFormUrlEncoded.ReadAsStringAsync();

        var response =
            await _httpClient.GetAsync($"v1/{ResourcePath}?{queryString}");

        if (response.IsSuccessStatusCode)
            weatherResponse = await response.Content.ReadFromJsonAsync<WeatherResponse?>();
        else
        {
            _logger.LogError($"Error calling {nameof(GetAsync)} from Weather Api Client: {response?.Content}");
            throw new HttpRequestException();
        }

        return weatherResponse;
    }
}