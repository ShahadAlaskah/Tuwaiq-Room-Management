using Shared.Domain;
using Shared.Ids;

namespace Domain.Domains;

public class AssetType : IBaseEntity
{
    public AssetType(string name, string? icon = null)
    {
        Name = name;
        Icon = icon;
    }

    public AssetTypeId Id { get; set; } = AssetTypeId.New();
    public string Name { get; set; }
    public string? Icon { get; set; }
}