using Widget.Contracts.Interfaces;
using Widget.Contracts.Models;

namespace Widget.Services;

public class LocalService : ILocalService
{
    private readonly ILocalRepository _localRepository;

    public LocalService(ILocalRepository localRepository)
    {
        _localRepository = localRepository;
    }

    public IReadOnlyCollection<LocalFile>? GetLocalFiles(string? q, int? pageSize)
    {
        var localFiles = _localRepository.GetLocalFiles();

        if (!string.IsNullOrWhiteSpace(q))
            localFiles = localFiles.Where(x => x.Name.ToLower().Contains(q.ToLower()));
        
        if (pageSize.HasValue && !string.IsNullOrWhiteSpace(pageSize.ToString()))
            localFiles = localFiles.Take(pageSize.Value);

        return localFiles.ToList();
    }
}