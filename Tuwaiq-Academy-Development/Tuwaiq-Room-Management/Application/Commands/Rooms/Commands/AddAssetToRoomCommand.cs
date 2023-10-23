using Shared.Ids;
using Shared.Interfaces;

namespace Application.Commands.Rooms.Commands;

public class AddAssetToRoomCommand : ICommand<AssetId>
{
    public AddAssetToRoomCommand(RoomId id, string code, AssetTypeId assetTypeId, DateOnly installedDate)
    {
        RoomId = id;
        Code = code;
        AssetTypeId = assetTypeId;
        InstalledDate = installedDate;
    }

    public RoomId RoomId { get; set; }
    public string Code { get; set; }
    public AssetTypeId AssetTypeId { get; set; }
    public DateOnly InstalledDate { get; set; }
}