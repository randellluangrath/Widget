using System.Collections.ObjectModel;
using NewsAPI.Models;

namespace Widget.Contracts.Interfaces;

public interface INewsApiClientWrapper
{
    Task<ReadOnlyCollection<Article>?> GetEverythingAsync(EverythingRequest everythingRequest);
}