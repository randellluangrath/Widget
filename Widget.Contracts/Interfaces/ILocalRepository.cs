using Widget.Contracts.Models;

namespace Widget.Contracts.Interfaces;

public interface ILocalRepository 
{
    IEnumerable<LocalFile> GetLocalFiles();
}