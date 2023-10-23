namespace SDK.Core.Commands;

public class UpdateFloorCommand 
{
    public UpdateFloorCommand(Guid id, string name)
    {
        Id = id;
        Name = name;
    }

    public Guid Id { get; set; }

    public string Name { get; set; }
}