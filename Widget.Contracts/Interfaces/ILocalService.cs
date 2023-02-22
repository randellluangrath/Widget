using Widget.Contracts.Models;

namespace Widget.Contracts.Interfaces;

public interface ILocalService
{
    IReadOnlyCollection<LocalApplication>? GetLocalApplication(string q, DateTime from, int page, int pageSize);
    IReadOnlyCollection<LocalFile>? GetLocalFiles(string q, int page, int pageSize);
}