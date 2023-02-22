using Microsoft.AspNetCore.Mvc;
using Widget.Contracts.Enums;
using Widget.Contracts.Interfaces;
using Widget.Contracts.Models;
using Widget.Web.Filters;

namespace Widget.Web.Controllers;

[Route("v1/[controller]")]
[ApiExplorerSettings(GroupName = "v1")]
[ApiController]
public class LocalController : WidgetBaseController<LocalController>
{
    private readonly ILocalService _localService;

    public LocalController(ILocalService localService, ILogger<LocalController> logger) : base(logger)
    {
        _localService = localService;
    }

    // 200
    // 400
    // 500
    [ApiKey]
    [HttpGet]
    public IActionResult Get([FromQuery] string q, [FromQuery] DateTime from,
        [FromQuery] int page, [FromQuery] int pageSize, [FromQuery] ResourceType resourceType)
    {
        if (resourceType == ResourceType.NoOp)
            return BadRequest($"Error: Parameter {nameof(resourceType)} is not supported.");
        
        IReadOnlyCollection<LocalApplication>? localApplications = null;
        IReadOnlyCollection<LocalFile>? localFile = null;
        
        try
        {
            switch (resourceType)
            {
                case ResourceType.Application:
                {
                    localApplications = _localService.GetLocalApplication(q, from, page, pageSize);
                    break;
                }
                case ResourceType.File:
                {
                    localFile = _localService.GetLocalFiles(q, page, pageSize);
                    break;
                }
                default:
                    return BadRequest($"Error: Parameter {nameof(resourceType)} is not supported.");
            }
        }
        catch (Exception ex)
        {
            LogError(nameof(Get), ex);
            return StatusCode(500);
        }

        return localApplications is not null ? Ok(localApplications) : Ok(localFile);
    }
}