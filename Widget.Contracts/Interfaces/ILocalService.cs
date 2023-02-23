using Widget.Contracts.Models;

namespace Widget.Contracts.Interfaces;

public interface ILocalService
{
    IReadOnlyCollection<LocalFile>? GetLocalFiles(string? q, int? pageSize);
}