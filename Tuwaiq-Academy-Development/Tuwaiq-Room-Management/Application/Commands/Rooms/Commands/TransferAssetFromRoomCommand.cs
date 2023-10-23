using Shared.Ids;
using Shared.Interfaces;

namespace Application.Commands.Rooms.Commands;

public class TransferAssetFromRoomCommand : ICommand
{
    public TransferAssetFromRoomCommand(AssetId assetId, RoomId fromRoomId, RoomId toRoomId)
    {
        AssetId = assetId;
        FromRoomId = fromRoomId;
        ToRoomId = toRoomId;
    }

    public RoomId ToRoomId { get; set; }

    public RoomId FromRoomId { get; set; }


    public AssetId AssetId { get; set; }
}