using Shared.Ids;
using Shared.Interfaces;

namespace Application.Commands.Rooms.Commands;

public class RemoveAssetFromRoomCommand : ICommand
{
    public RemoveAssetFromRoomCommand(RoomId id, AssetId assetId)
    {
        RoomId = id;
        AssetId = assetId;
    }

    public RoomId RoomId { get; set; }
    public AssetId AssetId { get; set; }
}