namespace SDK.Core.Commands;

public class UpdateRoomCommand 
{
    public UpdateRoomCommand(Guid id, string name, string code, Guid floorId, Guid buildingId, Guid roomTypeId, int capacity)
    {
        Id = id;
        Name = name;
        Code = code;
        FloorId = floorId;
        BuildingId = buildingId;
        RoomTypeId = roomTypeId;
        Capacity = capacity;
    }

    public Guid Id { get; set; }

    public string Name { get; set; }
    public string Code { get; set; }
    public Guid FloorId { get; set; }
    public Guid BuildingId { get; set; }
    public Guid RoomTypeId { get; set; }
    public int Capacity { get; set; }
}