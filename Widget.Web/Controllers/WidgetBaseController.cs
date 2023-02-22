using Microsoft.AspNetCore.Mvc;

namespace Widget.Web.Controllers;

public class WidgetBaseController<T> : ControllerBase
{
    private readonly ILogger<T> _logger;

    public WidgetBaseController(
        ILogger<T> logger
    )
    {
        _logger = logger;
    }

    protected void LogError(string method, Exception ex)
    {
        _logger.LogError($"Error: Unable to process {method}.\n {ex.Message}\n {ex.StackTrace}");
    }
}