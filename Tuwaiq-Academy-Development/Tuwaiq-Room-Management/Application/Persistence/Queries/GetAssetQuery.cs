using Application.Dto;
using Application.Interfaces;
using Domain.Domains;
using Shared.Interfaces;

namespace Application.Persistence.Queries;

public class GetAssetQuery : IQuery<PaginatedList<AssetDto>>
{
    public GetAssetQuery(ISpecification<Asset> specification, int? page = null, int? pageCount = null)
    {
        Specification = specification;
        Page = page;
        PageCount = pageCount;
    }

    public ISpecification<Asset> Specification { get; set; }
    public int? Page { get; set; } = 1;
    public int? PageCount { get; set; } = 10;
}