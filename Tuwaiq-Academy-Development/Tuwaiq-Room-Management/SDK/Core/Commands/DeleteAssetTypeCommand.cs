namespace SDK.Core.Commands;

public class DeleteAssetTypeCommand 
{
    public DeleteAssetTypeCommand(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; set; }
}