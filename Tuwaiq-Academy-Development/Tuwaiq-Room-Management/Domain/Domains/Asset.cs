using Shared.Domain;
using Shared.Ids;

namespace Domain.Domains;

public class Asset : IBaseEntity
{
    public Asset( string code,RoomId? roomId, AssetTypeId assetTypeId, DateOnly installedDate)
    {
        Code = code;
        RoomId = roomId;
        AssetTypeId = assetTypeId;
        InstalledDate = installedDate;
        AddedOn = DateTimeOffset.Now;
    }

    public AssetId Id { get; set; } = AssetId.New();
    public string Code { get; set; }
    public RoomId? RoomId { get; set; }
    public Room Room { get; set; } = null!;
    public AssetTypeId AssetTypeId { get; set; }
    public AssetType AssetType { get; set; } = null!;
    public DateOnly InstalledDate { get; set; }
    public DateTimeOffset AddedOn { get; init; }
}