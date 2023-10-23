using Shared.Ids;
using Shared.Interfaces;

namespace Application.Commands.Buildings.Commands;

public class DeleteBuildingCommand : ICommand
{
    public DeleteBuildingCommand(BuildingId id)
    {
        Id = id;
    }

    public BuildingId Id { get; set; }
}