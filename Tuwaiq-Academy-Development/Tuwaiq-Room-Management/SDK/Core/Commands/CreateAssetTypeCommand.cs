namespace SDK.Core.Commands;

public class CreateAssetTypeCommand 
{
    public CreateAssetTypeCommand(string name, string? icon = null)
    {
        Name = name;
        Icon = icon;
    }

    public Guid Id { get; set; }

    public string Name { get; set; }
    public string? Icon { get; set; }
}