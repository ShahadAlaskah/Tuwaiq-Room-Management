using Shared.Ids;
using Shared.Interfaces;

namespace Application.Commands.Rooms.Commands;

public class MarkRoomAsUnderMaintenanceCommand : ICommand
{
    public MarkRoomAsUnderMaintenanceCommand(RoomId id)
    {
        RoomId = id;
    }

    public RoomId RoomId { get; set; }
}