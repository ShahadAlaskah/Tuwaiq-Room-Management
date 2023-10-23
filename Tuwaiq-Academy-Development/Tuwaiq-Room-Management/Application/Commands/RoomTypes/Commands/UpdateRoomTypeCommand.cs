using Domain.Enums;
using Shared.Ids;
using Shared.Interfaces;

namespace Application.Commands.RoomTypes.Commands;

public class UpdateRoomTypeCommand : ICommand<RoomTypeId>
{
    public UpdateRoomTypeCommand(RoomTypeId id, string name,RoomTypeEnum roomTypeEnum)
    {
        Id = id;
        Name = name;
        RoomTypeEnum = roomTypeEnum;
    }

    public RoomTypeId Id { get; }

    public string Name { get; set; }
    public RoomTypeEnum RoomTypeEnum { get; set; }
}