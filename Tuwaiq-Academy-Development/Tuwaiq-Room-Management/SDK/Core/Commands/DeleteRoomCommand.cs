namespace SDK.Core.Commands;

public class DeleteRoomCommand 
{
    public DeleteRoomCommand(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; set; }
}