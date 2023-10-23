namespace SDK.Core.Commands;

public class CreateBuildingCommand 
{
    public CreateBuildingCommand(string name, string code )
    {
        Name = name;
        Code = code;
    }

    public Guid Id { get; set; } 

    public string Name { get; set; }
    public string Code { get; set; }
}