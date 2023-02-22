using Widget.Contracts.Models;

namespace Widget.Contracts.Interfaces;

public interface IWeatherApiClient
{
    Task<WeatherResponse?> GetAsync(string q);
}