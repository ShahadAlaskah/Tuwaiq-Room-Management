namespace SDK.Core.Commands;

public class DeleteFloorCommand 
{
    public DeleteFloorCommand(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; set; }
}