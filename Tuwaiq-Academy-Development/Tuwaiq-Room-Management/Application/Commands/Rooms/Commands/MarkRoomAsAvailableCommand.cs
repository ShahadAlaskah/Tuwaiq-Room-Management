using Shared.Ids;
using Shared.Interfaces;

namespace Application.Commands.Rooms.Commands;

public class MarkRoomAsAvailableCommand : ICommand
{
    public MarkRoomAsAvailableCommand(RoomId id)
    {
        RoomId = id;
    }

    public RoomId RoomId { get; set; }
}