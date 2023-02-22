using Widget.Contracts.Models;

namespace Widget.Contracts.Interfaces;

public interface IWeatherApiClient
{
    Task<WeatherResponse?> GetCurrentWeatherAsync(string q);
}