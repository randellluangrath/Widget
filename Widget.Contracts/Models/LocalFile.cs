using Widget.Contracts.Enums;

namespace Widget.Contracts.Models;

public class LocalFile
{
    public string Name { get; set; }
    public ResourceType Type { get; set; }
    public DateTime LastModified { get; set; }
}