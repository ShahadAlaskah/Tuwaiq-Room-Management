using Shared.Ids;
using Shared.Interfaces;

namespace Application.Commands.RoomTypes.Commands;

public class DeleteRoomTypeCommand : ICommand
{
    public DeleteRoomTypeCommand(RoomTypeId id)
    {
        Id = id;
    }

    public RoomTypeId Id { get; set; }
}