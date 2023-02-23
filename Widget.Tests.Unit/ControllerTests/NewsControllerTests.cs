using System.Collections.ObjectModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NewsAPI.Models;
using NUnit.Framework;
using Widget.Contracts.Interfaces;
using Widget.Web.Controllers;

namespace Widget.Tests.ControllerTests;

[TestFixture]
public class NewsControllerTests
{
    private readonly INewsApiClientWrapper _mockNewsApiClientWrapper = Mock.Of<INewsApiClientWrapper>();
    private readonly ILogger<NewsController> _mockLogger = Mock.Of<ILogger<NewsController>>();

    [Test]
    public async Task GetAsync_NoResultsFromClient_Returns404()
    {
        // Arrange
        var expectedStatusCode = 404;
        
        Mock.Get(_mockNewsApiClientWrapper).Setup(x => x.GetAsync(It.IsAny<EverythingRequest>()))
            .ReturnsAsync((ReadOnlyCollection<Article>?)null);

        var controller = new NewsController(_mockNewsApiClientWrapper, _mockLogger);
        
        // Act
        var actionResult = await controller.GetAsync(null, default, 0, 0);

        // Assert
        var result = actionResult as NotFoundResult;
        Assert.That(result?.StatusCode, Is.EqualTo(expectedStatusCode));
    }
    
    [Test]
    public async Task GetAsync_UnexpectedException_Returns500()
    {
        // Arrange
        var expectedStatusCode = 500;
        
        Mock.Get(_mockNewsApiClientWrapper).Setup(x => x.GetAsync(It.IsAny<EverythingRequest>()))
            .Throws<Exception>();

        var controller = new NewsController(_mockNewsApiClientWrapper, _mockLogger);
        
        // Act
        var actionResult = await controller.GetAsync(null, default, 0, 0);

        // Assert
        var result = actionResult as StatusCodeResult;
        Assert.That(result?.StatusCode, Is.EqualTo(expectedStatusCode));
    }
}