using Widget.Contracts.Interfaces;
using Widget.Contracts.Models;

namespace Widget.Infrastructure.Repositories;

public class LocalRepository : ILocalRepository
{
    private readonly IEnumerable<LocalApplication> _localApplications;
    private readonly IEnumerable<LocalFile> _localFile;

    private readonly Random _random = new();
    private readonly string[] _types = { "CSV", "PDF", "TXT" };

    public LocalRepository()
    {
        _localApplications = Enumerable.Range(1, 10)
            .Select(x => new LocalApplication { Name = $"App {x}", LastModified = RandomDay() });

        _localFile = Enumerable.Range(1, 10)
            .Select(x => new LocalFile()
            {
                Name = $"File {x}", LastModified = RandomDay(),
                Type = _types[_random.Next(0, _types.Length)]
            });
    }

    public IEnumerable<LocalApplication> GetLocalApplication()
    {
        return _localApplications;
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