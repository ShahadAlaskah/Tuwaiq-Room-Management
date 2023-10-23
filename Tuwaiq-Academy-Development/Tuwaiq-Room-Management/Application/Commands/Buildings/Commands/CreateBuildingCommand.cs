using Shared.Ids;
using Shared.Interfaces;

namespace Application.Commands.Buildings.Commands;

public class CreateBuildingCommand : ICommand<BuildingId>
{
    public CreateBuildingCommand(string name, string code )
    {
        Name = name;
        Code = code;
    }

    public BuildingId Id { get; } = BuildingId.New();

    public string Name { get; set; }
    public string Code { get; set; }
}