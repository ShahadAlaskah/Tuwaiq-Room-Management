using Shared.Ids;
using Shared.Interfaces;

namespace Application.Commands.Rooms.Commands;

public class DeleteRoomCommand : ICommand
{
    public DeleteRoomCommand(RoomId id)
    {
        RoomId = id;
    }

    public RoomId RoomId { get; set; }
}