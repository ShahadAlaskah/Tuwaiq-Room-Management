namespace SDK.Core.Commands;

public class AddAssetToRoomCommand
{
    public AddAssetToRoomCommand(Guid id, string code, Guid assetTypeId, DateOnly installedDate)
    {
        RoomId = id;
        Code = code;
        AssetTypeId = assetTypeId;
        InstalledDate = installedDate;
    }

    public Guid RoomId { get; set; }
    public string Code { get; set; }
    public Guid AssetTypeId { get; set; }
    public DateOnly InstalledDate { get; set; }
}