using Shared.Ids;
using Shared.Interfaces;

namespace Application.Commands.Rooms.Commands;

public class CreateRoomCommand : ICommand<RoomId>
{
    public CreateRoomCommand(string name, string code, FloorId floorId, BuildingId buildingId, RoomTypeId roomTypeId, int capacity)
    {
        Name = name;
        Code = code;
        FloorId = floorId;
        BuildingId = buildingId;
        RoomTypeId = roomTypeId;
        Capacity = capacity;
    }

    public RoomId RoomId { get; } = RoomId.New();

    
    public string Name { get; set; }
    public string Code { get; set; }
    public FloorId FloorId { get; set; }
    public BuildingId BuildingId { get; set; }
    public RoomTypeId RoomTypeId { get; set; }
    public int Capacity { get; set; }
}