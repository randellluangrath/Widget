using Widget.Contracts.Enums;
using Widget.Contracts.Interfaces;
using Widget.Contracts.Models;

namespace Widget.Infrastructure.Repositories;

public class LocalRepository : ILocalRepository
{
    private readonly IEnumerable<LocalFile> _localFile;

    private readonly Random _random = new();

    public LocalRepository()
    {
        var values = Enum.GetValues(typeof(ResourceType));

        _localFile = Enumerable.Range(1, 50)
            .Select(x => new LocalFile()
            {
                Name = $"File {x}", LastModified = RandomDay(),
                Type = (ResourceType)values.GetValue(_random.Next(values.Length))
            });
    }

    public IEnumerable<LocalFile> GetLocalFiles()
    {
        return _localFile;
    }

    private DateTime RandomDay()
    {
        var start = new DateTime(1995, 1, 1);
        var range = (DateTime.Today - start).Days;
        return start.AddDays(_random.Next(range));
    }
}