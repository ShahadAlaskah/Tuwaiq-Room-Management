using Shared.Domain;
using Shared.Ids;

namespace Domain.Domains;

public class Building : IBaseEntity
{
    public Building(string name, string code)
    {
        Name = name;
        Code = code;
    }

    public BuildingId Id { get; set; } = BuildingId.New();
    public string Name { get; set; }
    public string Code { get; set; }
}