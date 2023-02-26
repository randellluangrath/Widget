using Microsoft.AspNetCore.Mvc;
using Widget.Contracts.Interfaces;
using Widget.Contracts.Models;
using Widget.Web.Filters;

namespace Widget.Web.Controllers;

[Route("v1/[controller]")]
[ApiExplorerSettings(GroupName = "v1")]
[ApiController]
public class WeatherController : WidgetBaseController<WeatherController>
{
    private readonly IWeatherApiClient _weatherApiClient;

    public WeatherController(IWeatherApiClient weatherApiClient, ILogger<WeatherController> logger) : base(logger)
    {
        _weatherApiClient = weatherApiClient;
    }
    
    [ApiKey]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAsync([FromQuery] string q)
    {
        if (string.IsNullOrWhiteSpace(q))
            return BadRequest($"Error: Parameter {nameof(q)} is missing.");

        WeatherResponse? result;

        try
        {
            result = await _weatherApiClient.GetAsync(q);

            if (result is null)
                return NotFound();
        }
        catch (Exception ex)
        {
            LogError(nameof(GetAsync), ex);
            return StatusCode(500);
        }

        return Ok(result);
    }
}