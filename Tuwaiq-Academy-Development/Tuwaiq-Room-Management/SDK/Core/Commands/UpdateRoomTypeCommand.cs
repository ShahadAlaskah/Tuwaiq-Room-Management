using Domain.Enums;

namespace SDK.Core.Commands;

public class UpdateRoomTypeCommand
{
    public UpdateRoomTypeCommand(Guid id, string name,RoomTypeEnum roomTypeEnum)
    {
        Id = id;
        Name = name;
        RoomTypeEnum = roomTypeEnum;
    }

    public Guid Id { get; }

    public string Name { get; set; }
    public RoomTypeEnum RoomTypeEnum { get; set; }
}