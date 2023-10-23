namespace SDK.Core.Commands;

public class RemoveAssetFromRoomCommand 
{
    public RemoveAssetFromRoomCommand(Guid id, Guid assetId)
    {
        RoomId = id;
        AssetId = assetId;
    }

    public Guid RoomId { get; set; }
    public Guid AssetId { get; set; }
}