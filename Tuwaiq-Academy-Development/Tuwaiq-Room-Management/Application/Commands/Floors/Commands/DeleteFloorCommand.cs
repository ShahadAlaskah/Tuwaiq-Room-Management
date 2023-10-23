using Shared.Ids;
using Shared.Interfaces;

namespace Application.Commands.Floors.Commands;

public class DeleteFloorCommand : ICommand
{
    public DeleteFloorCommand(FloorId id)
    {
        Id = id;
    }

    public FloorId Id { get; set; }
}