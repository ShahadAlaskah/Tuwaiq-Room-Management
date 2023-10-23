using Application.Handlers;
using Domain.Domains;
using Shared.Ids;

namespace Application.Specifications;

public class GetFloorSpecification : BaseSpecification<Floor>
{
    public GetFloorSpecification(string? query = null ,bool all = false) : base(
        item => (string.IsNullOrEmpty(query)
                 || item.Name.ToString()!.ToLower().Contains(query.ToLower())
            )
    )
    {
        AddOrderBy(s => s.Id);
    }

    public GetFloorSpecification(FloorId id) : base(item => item.Id == id)
    {
        AddOrderBy(s => s.Id);
    }
}