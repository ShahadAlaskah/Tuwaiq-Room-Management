namespace SDK.Core.Commands;

public class DeleteBuildingCommand 
{
    public DeleteBuildingCommand(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; set; }
}