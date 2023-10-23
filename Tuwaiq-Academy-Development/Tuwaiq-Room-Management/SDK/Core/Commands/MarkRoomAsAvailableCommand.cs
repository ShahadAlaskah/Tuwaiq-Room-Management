namespace SDK.Core.Commands;

public class MarkRoomAsAvailableCommand 
{
    public MarkRoomAsAvailableCommand(Guid id)
    {
        RoomId = id;
    }

    public Guid RoomId { get; set; }
}