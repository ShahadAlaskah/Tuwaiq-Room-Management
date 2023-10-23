using Shared.Ids;
using Shared.Interfaces;

namespace Application.Commands.AssetTypes.Commands;

public class UpdateAssetTypeCommand : ICommand<AssetTypeId>
{
    public UpdateAssetTypeCommand(AssetTypeId id, string name, string? icon=null)
    {
        Id = id;
        Name = name;
        Icon = icon;
    }

    public AssetTypeId Id { get; }

    public string Name { get; set; }
    public string? Icon { get; set; }
}