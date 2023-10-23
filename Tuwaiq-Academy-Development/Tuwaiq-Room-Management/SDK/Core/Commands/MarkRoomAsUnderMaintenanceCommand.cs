namespace SDK.Core.Commands;

public class MarkRoomAsUnderMaintenanceCommand 
{
    public MarkRoomAsUnderMaintenanceCommand(Guid id)
    {
        RoomId = id;
    }

    public Guid RoomId { get; set; }
}