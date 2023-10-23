using Domain.Enums;
using Shared.Ids;
using Shared.Interfaces;

namespace Application.Commands.RoomTypes.Commands;

public class CreateRoomTypeCommand : ICommand<RoomTypeId>
{
    public CreateRoomTypeCommand(string name ,RoomTypeEnum roomTypeEnum)
    {
        Name = name;
        RoomTypeEnum = roomTypeEnum;
    }

    public RoomTypeId Id { get; } = RoomTypeId.New();

    public string Name { get; set; }
    public RoomTypeEnum RoomTypeEnum { get; set; }
}