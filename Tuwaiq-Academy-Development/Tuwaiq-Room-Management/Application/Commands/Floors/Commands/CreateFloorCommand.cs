using Shared.Ids;
using Shared.Interfaces;

namespace Application.Commands.Floors.Commands;

public class CreateFloorCommand : ICommand<FloorId>
{
    public CreateFloorCommand(string name )
    {
        Name = name;
    }

    public FloorId Id { get; } = FloorId.New();

    public string Name { get; set; }
}