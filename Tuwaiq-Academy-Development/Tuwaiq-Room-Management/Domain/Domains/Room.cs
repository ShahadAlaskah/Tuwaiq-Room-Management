using Domain.Events;
using Shared.Base;
using Shared.Domain;
using Shared.Ids;
using Shared.Interfaces.ValidationErrors;

namespace Domain.Domains;

public class Room : BaseEntity
{
    public Room(string name, string code, FloorId floorId, BuildingId buildingId, RoomTypeId roomTypeId, int capacity)
    {
        Name = name;
        Code = code;
        FloorId = floorId;
        BuildingId = buildingId;
        RoomTypeId = roomTypeId;
        Capacity = capacity;
    }

    public RoomId Id { get; set; } = RoomId.New();

    public string Name { get; set; }
    public string Code { get; set; }
    public FloorId FloorId { get; set; }
    public Floor Floor { get; set; } = null!;
    public BuildingId BuildingId { get; set; }
    public Building Building { get; set; } = null!;
    public RoomTypeId RoomTypeId { get; set; }
    public RoomType RoomType { get; set; } = null!;
    public int Capacity { get; set; }
    public bool IsUnderMaintenance { get; private set; } = false;
    private IList<Asset> _assets = new List<Asset>();

    public IReadOnlyCollection<Asset> Assets => _assets.ToList();

    public Result<AssetId> AddAsset(string code, AssetTypeId assetTypeId, DateOnly installedDate)
    {
        var asset = new Asset(code, Id, assetTypeId, installedDate);
        _assets.Add(asset);

        return Result.Ok(asset.Id);
    }

    public Result RemoveAsset(AssetId id)
    {
        
        var item = _assets.FirstOrDefault(x => x.Id == id);
        if (item == null) return Result.Fail(Error.NotFound(id.Value.ToString()));

        item.RoomId = null;

        return Result.Ok();
    }

    public Result TransferAsset(RoomId id, AssetId assetId)
    {
        var item = _assets.FirstOrDefault(x => x.Id == assetId);

        if (item == null) return Result.Fail(Error.NotFound(id.Value.ToString()));

        item.RoomId = id;

        PublishEvent(new AssetTransferredEvent(Id, id));

        return Result.Ok();
    }

    public Result MarkAsUnderMaintenance()
    {
        if (IsUnderMaintenance) return Result.Fail(Error.Invalid("Room is already under maintenance"));

        IsUnderMaintenance = true;

        //TODO: Publish event
        return Result.Ok();
    }

    public Result MarkAsAvailable()
    {
        if (!IsUnderMaintenance) return Result.Fail(Error.Invalid("Room is not under maintenance"));

        IsUnderMaintenance = false;
        //TODO: Publish event

        return Result.Ok();
    }
}