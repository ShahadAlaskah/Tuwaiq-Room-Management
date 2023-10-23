using Application.Handlers;
using Domain.Domains;
using Shared.Ids;

namespace Application.Specifications;

public class GetRoomTypeSpecification : BaseSpecification<RoomType>
{
    public GetRoomTypeSpecification(string? query = null) : base(
        item => (string.IsNullOrEmpty(query)
                 || item.Name.ToString()!.ToLower().Contains(query.ToLower())
                 || item.RoomTypeEnum.ToString()!.ToLower().Contains(query.ToLower())
            )
    )
    {
        AddOrderBy(s => s.Id);
    }

    public GetRoomTypeSpecification(RoomTypeId id) : base(item => item.Id == id)
    {
        AddOrderBy(s => s.Id);
    }
}