using Shared.Ids;
using Shared.Interfaces;

namespace Application.Commands.AssetTypes.Commands;

public class CreateAssetTypeCommand : ICommand<AssetTypeId>
{
    public CreateAssetTypeCommand(string name, string? icon = null)
    {
        Name = name;
        Icon = icon;
    }

    public AssetTypeId Id { get; } = AssetTypeId.New();

    public string Name { get; set; }
    public string? Icon { get; set; }
}