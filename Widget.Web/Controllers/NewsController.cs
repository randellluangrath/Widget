using System.Collections.ObjectModel;
using Microsoft.AspNetCore.Mvc;
using NewsAPI.Constants;
using NewsAPI.Models;
using Widget.Contracts.Interfaces;
using Widget.Web.Filters;

namespace Widget.Web.Controllers;

[Route("v1/[controller]")]
[ApiExplorerSettings(GroupName = "v1")]
[ApiController]
public class NewsController : WidgetBaseController<NewsController>
{
    private readonly INewsApiClientWrapper _newsApiClientWrapper;

    public NewsController(INewsApiClientWrapper newsApiClientWrapper, ILogger<NewsController> logger) : base(logger)
    {
        _newsApiClientWrapper = newsApiClientWrapper;
    }

    // 200
    // 404
    // 500
    [ApiKey]
    [HttpGet]
    public async Task<IActionResult> GetAsync([FromQuery] string q, [FromQuery] DateTime from,
        [FromQuery] int page, [FromQuery] int pageSize)
    {
        var everythingRequest = new EverythingRequest
        {
            SortBy = SortBys.Popularity,
            Language = Languages.EN,
        };

        if (!string.IsNullOrWhiteSpace(q))
            everythingRequest.Q = q;

        if (from != DateTime.MinValue)
            everythingRequest.From = from;

        if (!string.IsNullOrWhiteSpace(page.ToString()))
            everythingRequest.Page = page;

        if (!string.IsNullOrWhiteSpace(pageSize.ToString()))
            everythingRequest.PageSize = pageSize;

        ReadOnlyCollection<Article>? result;

        try
        {
            result = await _newsApiClientWrapper.GetAsync(everythingRequest);

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