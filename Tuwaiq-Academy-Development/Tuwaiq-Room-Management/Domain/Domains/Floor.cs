using Domain.Base;
using Shared.Domain;
using Shared.Ids;

namespace Domain.Domains;

public class Floor : IBaseEntity, IAudiTable
{
    public Floor(string name)
    {
        Name = name;
    }

    public FloorId Id { get; set; } = FloorId.New();
    public string Name { get; set; }
}