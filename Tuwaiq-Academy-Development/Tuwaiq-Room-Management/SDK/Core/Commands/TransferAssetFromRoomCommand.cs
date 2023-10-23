namespace SDK.Core.Commands;

public class TransferAssetFromRoomCommand 
{
    public TransferAssetFromRoomCommand(Guid assetId, Guid fromRoomId, Guid toRoomId)
    {
        AssetId = assetId;
        FromRoomId = fromRoomId;
        ToRoomId = toRoomId;
    }

    public Guid ToRoomId { get; set; }

    public Guid FromRoomId { get; set; }
    
    public Guid AssetId { get; set; }
}