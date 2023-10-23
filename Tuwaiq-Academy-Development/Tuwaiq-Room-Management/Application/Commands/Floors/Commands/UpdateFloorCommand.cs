using Shared.Ids;
using Shared.Interfaces;

namespace Application.Commands.Floors.Commands;

public class UpdateFloorCommand : ICommand<FloorId>
{
    public UpdateFloorCommand(FloorId id, string name)
    {
        Id = id;
        Name = name;
    }

    public FloorId Id { get; }

    public string Name { get; set; }
}