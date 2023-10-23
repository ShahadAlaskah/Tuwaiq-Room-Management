using Application;
using Application.Dto;
using Application.Interfaces;
using Application.Persistence.Queries;
using Application.Specifications;
using Domain.Domains;
using LazyCache;
using Mapster;
using Microsoft.Extensions.Options;
using Shared.Base;
using Shared.Interfaces;
using Shared.Settings;

namespace Infrastructure.Persistence.QueryHandlers;

public class GetAssetTypeQueryHandler : IQueryHandler<GetAssetTypeQuery, PaginatedList<AssetTypeDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAppCache _cache;

    public GetAssetTypeQueryHandler(IUnitOfWork unitOfWork, IAppCache cache)
    {
        _unitOfWork = unitOfWork;
        _cache = cache;
    }

    public async Task<Result<PaginatedList<AssetTypeDto>>> Handle(GetAssetTypeQuery request,
        CancellationToken cancellationToken)
    {
        var result = _cache.GetOrAdd(nameof(Domain.Domains.AssetType), cache =>
        {
            cache.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10);
            return _unitOfWork.AssetTypes!.FindWithSpecificationPattern(new GetAssetTypeSpecification()).ToList();
        });

        var count = _unitOfWork.AssetTypes!.FindWithSpecificationPattern(result, request.Specification).Count();
        var items = _unitOfWork.AssetTypes!.FindWithSpecificationPattern(result, request.Specification)
            .Skip(((request.Page ?? 1) - 1) * (request.PageCount ?? 10)).Take(request.PageCount ?? 10).ToList();

        var assetTypeDtos = items.AsQueryable().ProjectToType<AssetTypeDto>().ToList();
        var paginatedList = await PaginatedList<AssetTypeDto>.CreateAsync(assetTypeDtos, count, request.Page ?? 1, request.PageCount ?? 10);
        return Result.Ok<PaginatedList<AssetTypeDto>>(paginatedList);
    }
}