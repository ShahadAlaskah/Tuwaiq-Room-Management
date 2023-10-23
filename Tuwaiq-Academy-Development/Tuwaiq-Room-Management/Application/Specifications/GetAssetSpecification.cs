using Application.Handlers;
using Domain.Domains;
using Shared.Ids;

namespace Application.Specifications;

public class GetAssetSpecification : BaseSpecification<Asset>
{
    public GetAssetSpecification(string? query = null, bool all = false) : base(
        item =>
            (string.IsNullOrEmpty(query)
             || item.Room.Name.ToString()!.ToLower().Contains(query.ToLower())
             || item.Room.Code.ToString()!.ToLower().Contains(query.ToLower())
             || item.AssetType.Name.ToString()!.ToLower().Contains(query.ToLower())
            )
    )
    {
        AddOrderBy(s => s.Id);
        AddInclude(s => s.AssetType);
        AddInclude(s => s.Room);
    }
    public GetAssetSpecification(RoomId roomId, string? query = null) : base(
        item =>
            item.RoomId == roomId &&
            (string.IsNullOrEmpty(query)
             || item.Room.Name.ToString()!.ToLower().Contains(query.ToLower())
             || item.Room.Code.ToString()!.ToLower().Contains(query.ToLower())
             || item.AssetType.Name.ToString()!.ToLower().Contains(query.ToLower())
            )
    )
    {
        AddOrderBy(s => s.Id);
        AddInclude(s => s.AssetType);
        AddInclude(s => s.Room);
    }

    public GetAssetSpecification(RoomId roomId, AssetId id) : base(item => item.Id == id && item.RoomId == roomId)
    {
        AddOrderBy(s => s.Id);
        AddInclude(s => s.AssetType);
        AddInclude(s => s.Room);
    }
}