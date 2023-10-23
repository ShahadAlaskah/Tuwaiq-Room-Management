using Application.Handlers;
using Domain.Domains;
using Shared.Ids;

namespace Application.Specifications;

public class GetBuildingSpecification : BaseSpecification<Building>
{
    public GetBuildingSpecification(string? query = null) : base(
        item => (string.IsNullOrEmpty(query)
                 || item.Name.ToString()!.ToLower().Contains(query.ToLower())
                 || item.Code.ToLower().Contains(query.ToLower())
            )
    )
    {
        AddOrderBy(s => s.Id);
    }

    public GetBuildingSpecification(BuildingId id) : base(item => item.Id == id)
    {
        AddOrderBy(s => s.Id);
    }
}