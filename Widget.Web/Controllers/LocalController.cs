using Microsoft.AspNetCore.Mvc;
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

    [ApiKey]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult Get([FromQuery] string? q, int? pageSize)
    {
        IReadOnlyCollection<LocalFile>? localFiles;

        try
        {
            localFiles = _localService.GetLocalFiles(q, pageSize);
        }
        catch (Exception ex)
        {
            LogError(nameof(Get), ex);
            return StatusCode(500);
        }

        return Ok(localFiles);
    }
}