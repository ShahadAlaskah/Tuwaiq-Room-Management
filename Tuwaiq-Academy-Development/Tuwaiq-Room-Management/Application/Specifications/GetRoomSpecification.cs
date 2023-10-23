using Application.Handlers;
using Domain.Domains;
using Shared.Ids;

namespace Application.Specifications;

public class GetRoomSpecification : BaseSpecification<Room>
{
    public GetRoomSpecification(string? query = null) : base(
        item => (string.IsNullOrEmpty(query)
                 || item.Name.ToString()!.ToLower().Contains(query.ToLower())
                 || item.Capacity.ToString()!.ToLower().Contains(query.ToLower())
                 || item.Code.ToString()!.ToLower().Contains(query.ToLower())
            )
    )
    {
        AddOrderBy(s => s.Id);
        AddInclude(s=>s.Assets);
        AddInclude(s=>s.Building);
        AddInclude(s=>s.RoomType);
    }

    public GetRoomSpecification(RoomId id) : base(item => item.Id == id)
    {
        AddOrderBy(s => s.Id);
        AddInclude(s=>s.Assets);
        AddInclude(s=>s.Building);
        AddInclude(s=>s.RoomType);
    }
}