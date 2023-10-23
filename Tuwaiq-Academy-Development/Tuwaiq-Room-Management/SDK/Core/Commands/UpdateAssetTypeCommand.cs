namespace SDK.Core.Commands;

public class UpdateAssetTypeCommand 
{
    public UpdateAssetTypeCommand(Guid id, string name, string? icon=null)
    {
        Id = id;
        Name = name;
        Icon = icon;
    }

    public Guid Id { get; }

    public string Name { get; set; }
    public string? Icon { get; set; }
}