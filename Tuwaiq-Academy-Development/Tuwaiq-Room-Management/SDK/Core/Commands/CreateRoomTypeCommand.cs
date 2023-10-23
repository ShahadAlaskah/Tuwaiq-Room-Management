using Domain.Enums;

namespace SDK.Core.Commands;

public class CreateRoomTypeCommand 
{
    public CreateRoomTypeCommand(string name ,RoomTypeEnum roomTypeEnum)
    {
        Name = name;
        RoomTypeEnum = roomTypeEnum;
    }

    public Guid Id { get; } 

    public string Name { get; set; }
    public RoomTypeEnum RoomTypeEnum { get; set; }
}