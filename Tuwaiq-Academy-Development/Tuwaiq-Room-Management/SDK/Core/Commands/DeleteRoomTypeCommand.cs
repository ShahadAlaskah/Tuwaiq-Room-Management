namespace SDK.Core.Commands;

public class DeleteRoomTypeCommand 
{
    public DeleteRoomTypeCommand(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; set; }
}