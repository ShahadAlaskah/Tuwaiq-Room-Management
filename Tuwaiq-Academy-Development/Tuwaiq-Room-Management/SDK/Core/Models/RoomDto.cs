namespace SDK.Core.Models;

public class RoomDto 
{
    public Guid Id { get; set; }

    public string Name { get; set; }
    public string Code { get; set; }
    public Guid FloorId { get; set; }
    public FloorDto Floor { get; set; } = null!;
    public Guid BuildingId { get; set; }
    public BuildingDto Building { get; set; } = null!;
    public Guid RoomTypeId { get; set; }
    public RoomTypeDto RoomType { get; set; } = null!;
    public int Capacity { get; set; }
    public bool IsUnderMaintenance { get; set; }
    private IList<AssetDto> Assets { get; set; }
}