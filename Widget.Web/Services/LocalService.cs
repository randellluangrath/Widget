using Widget.Contracts.Interfaces;
using Widget.Contracts.Models;

namespace Widget.Web.Services;

public class LocalService : ILocalService
{
    private readonly ILocalRepository _localRepository;

    public LocalService(ILocalRepository localRepository)
    {
        _localRepository = localRepository;
    }

    public IReadOnlyCollection<LocalApplication>? GetLocalApplication(string q, DateTime from, int page, int pageSize)
    {
        var localApplications = _localRepository.GetLocalApplication();
        return localApplications.Where(x => x.Name.ToLower().Contains(q.ToLower())).ToList();
    }

    public IReadOnlyCollection<LocalFile>? GetLocalFiles(string q, int page, int pageSize)
    {
        var localFiles = _localRepository.GetLocalFiles();
        return localFiles.Where(x => x.Name.ToLower().Contains(q.ToLower())).ToList();
    }
}