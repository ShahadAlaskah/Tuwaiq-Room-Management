using Application.Handlers;
using Domain.Domains;
using Shared.Ids;

namespace Application.Specifications;

public class GetAssetTypeSpecification : BaseSpecification<AssetType>
{
    public GetAssetTypeSpecification(string? query = null) : base(
        item => (string.IsNullOrEmpty(query)
                 || item.Name.ToLower().Contains(query.ToLower())
            )
    )
    {
        AddOrderBy(s => s.Id);
    }

    public GetAssetTypeSpecification(AssetTypeId id) : base(item => item.Id == id)
    {
        AddOrderBy(s => s.Id);
    }
}