using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using Widget.Contracts.Interfaces;
using Widget.Contracts.Models;
using Widget.Web.Controllers;

namespace Widget.Tests.ControllerTests;

[TestFixture]
public class WeatherControllerTests
{
    private readonly IWeatherApiClient _mockWeatherApiClient = Mock.Of<IWeatherApiClient>();
    private readonly ILogger<WeatherController> _mockLogger = Mock.Of<ILogger<WeatherController>>();

    [Test]
    public async Task GetAsync_NullQueryParameter_Returns400()
    {
        // Arrange
        var expectedStatusCode = 400;
        
        var controller = new WeatherController(_mockWeatherApiClient, _mockLogger);
        
        // Act
        var actionResult = await controller.GetAsync(null);

        // Assert
        var result = actionResult as BadRequestObjectResult;
        Assert.Multiple(() =>
        {
            Assert.That(result?.StatusCode, Is.EqualTo(expectedStatusCode));
            Assert.That(result?.Value, Is.EqualTo("Error: Parameter q is missing."));
        });
    }

    [Test]
    public async Task GetAsync_NullQueryParameter_Returns404()
    {
        // Arrange
        var expectedStatusCode = 404;
        
        Mock.Get(_mockWeatherApiClient).Setup(x => x.GetAsync(It.IsAny<string>()))
            .ReturnsAsync((WeatherResponse?)null);
    
        var controller = new WeatherController(_mockWeatherApiClient, _mockLogger);
        
        // Act
        var actionResult = await controller.GetAsync("test");
    
        // Assert
        var result = actionResult as NotFoundResult;
        Assert.That(result?.StatusCode, Is.EqualTo(expectedStatusCode));
    }
    
    [Test]
    public async Task GetAsync_UnexpectedException_Returns500()
    {
        // Arrange
        var expectedStatusCode = 500;
        
        Mock.Get(_mockWeatherApiClient).Setup(x => x.GetAsync(It.IsAny<string>()))
            .Throws<Exception>();
    
        var controller = new WeatherController(_mockWeatherApiClient, _mockLogger);
        
        // Act
        var actionResult = await controller.GetAsync("test");
    
        // Assert
        var result = actionResult as StatusCodeResult;
        Assert.That(result?.StatusCode, Is.EqualTo(expectedStatusCode));
    }
}