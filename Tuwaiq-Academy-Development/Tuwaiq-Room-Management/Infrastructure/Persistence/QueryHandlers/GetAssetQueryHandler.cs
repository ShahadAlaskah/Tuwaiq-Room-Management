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

public class GetAssetQueryHandler : IQueryHandler<GetAssetQuery, PaginatedList<AssetDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAppCache _cache;
    private readonly IOptionsMonitor<CacheSettings> _cacheSettings;

    public GetAssetQueryHandler(IUnitOfWork unitOfWork, IAppCache cache, IOptionsMonitor<CacheSettings> cacheSettings)
    {
        _unitOfWork = unitOfWork;
        _cache = cache;
        _cacheSettings = cacheSettings;
    }

    public async Task<Result<PaginatedList<AssetDto>>> Handle(GetAssetQuery request,
        CancellationToken cancellationToken)
    {
        var useCache = _cacheSettings.CurrentValue.UseCache;
        var data = useCache ? GetCachedData(request) : GetDatabaseData(request);
        
        var items = data.Data.ProjectToType<AssetDto>().ToList();
        
        return Result.Ok(await PaginatedList<AssetDto>.CreateAsync(items, data.Count, request.Page ?? 1, request.PageCount ?? 10));
    }

    private (IQueryable<Asset> Data, int Count) GetCachedData(GetAssetQuery request)
    {
        var cacheKey = nameof(Asset);
        var cache = _cache.GetOrAdd(cacheKey, () =>
        {
            var data = _unitOfWork.Assets!
                .FindWithSpecificationPattern(new GetAssetSpecification(all:true))
                .ToList();
            return data;
        }, DateTimeOffset.Now.AddSeconds(_cacheSettings.CurrentValue.ExpireSeconds));

        var data = _unitOfWork.Assets!.FindWithSpecificationPattern(cache.ToList(), request.Specification).Skip(((request.Page ?? 1) - 1) * (request.PageCount ?? 10)).Take(request.PageCount ?? 10);

        var count = _unitOfWork.Assets!.FindWithSpecificationPattern(cache.ToList(), request.Specification).Count();

        return (data, count);
    }

    private (IQueryable<Asset> Data, int Count) GetDatabaseData(GetAssetQuery request)
    {
        var data = _unitOfWork.Assets!.FindWithSpecificationPattern(request.Specification)
            .Skip(((request.Page ?? 1) - 1) * (request.PageCount ?? 10)).Take(request.PageCount ?? 10);
        var count = _unitOfWork.Assets!.FindWithSpecificationPattern(request.Specification).Count();

        return (data,count);
    }
}
