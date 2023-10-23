using Shared.Ids;
using Shared.Interfaces;

namespace Application.Commands.AssetTypes.Commands;

public class DeleteAssetTypeCommand : ICommand
{
    public DeleteAssetTypeCommand(AssetTypeId id)
    {
        Id = id;
    }

    public AssetTypeId Id { get; set; }
}