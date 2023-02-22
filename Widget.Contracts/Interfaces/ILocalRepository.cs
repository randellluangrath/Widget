using Widget.Contracts.Models;

namespace Widget.Contracts.Interfaces;

public interface ILocalRepository 
{
    IEnumerable<LocalApplication> GetLocalApplication();
    IEnumerable<LocalFile> GetLocalFiles();
}