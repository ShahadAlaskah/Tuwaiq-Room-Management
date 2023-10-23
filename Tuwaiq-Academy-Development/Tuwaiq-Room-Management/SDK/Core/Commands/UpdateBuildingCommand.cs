namespace SDK.Core.Commands;

public class UpdateBuildingCommand 
{
    public UpdateBuildingCommand(Guid id, string name, string code)
    {
        Id = id;
        Name = name;
        Code = code;
    }

    public Guid Id { get; }

    public string Name { get; set; }
    public string Code { get; set; }
}