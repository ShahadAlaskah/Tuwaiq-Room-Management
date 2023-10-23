using Shared.Ids;
using Shared.Interfaces;

namespace Application.Commands.Buildings.Commands;

public class UpdateBuildingCommand : ICommand<BuildingId>
{
    public UpdateBuildingCommand(BuildingId id, string name, string code)
    {
        Id = id;
        Name = name;
        Code = code;
    }

    public BuildingId Id { get; }

    public string Name { get; set; }
    public string Code { get; set; }
}