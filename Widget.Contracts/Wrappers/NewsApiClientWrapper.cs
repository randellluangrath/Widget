using System.Collections.ObjectModel;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NewsAPI;
using NewsAPI.Constants;
using NewsAPI.Models;
using Widget.Contracts.Interfaces;
using Widget.Contracts.Models;

namespace Widget.Contracts.Wrappers;

public class NewsApiClientWrapper : INewsApiClientWrapper
{
    private readonly NewsApiClient _newsApiClient;
    private readonly ILogger<NewsApiClientWrapper> _logger;

    public NewsApiClientWrapper(IOptions<ApplicationOptions> applicationOptions, ILogger<NewsApiClientWrapper> logger)
    {
        _logger = logger;
        _newsApiClient = new NewsApiClient(applicationOptions.Value.NewsApiKey);
    }

    public async Task<ReadOnlyCollection<Article>?> GetAsync(EverythingRequest everythingRequest)
    {
        IList<Article>? articles;

        var response = await _newsApiClient.GetEverythingAsync(everythingRequest);

        if (response.Status == Statuses.Ok)
            articles = response.Articles;
        else
        {
            _logger.LogError($"Error calling {nameof(GetAsync)} from News Api Client: {response.Error}");
            throw new HttpRequestException();
        }

        return articles.AsReadOnly();
    }
}