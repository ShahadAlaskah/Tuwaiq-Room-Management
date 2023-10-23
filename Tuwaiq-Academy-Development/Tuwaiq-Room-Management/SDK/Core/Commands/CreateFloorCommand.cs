namespace SDK.Core.Commands;

public class CreateFloorCommand 
{
    public CreateFloorCommand(string name )
    {
        Name = name;
    }

    public Guid Id { get; set; } 

    public string Name { get; set; }
}