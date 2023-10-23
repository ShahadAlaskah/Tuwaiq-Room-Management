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

public class GetFloorQueryHandler : IQueryHandler<GetFloorQuery, PaginatedList<FloorDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAppCache _cache;
    private readonly IOptionsMonitor<CacheSettings> _cacheSettings;

    public GetFloorQueryHandler(IUnitOfWork unitOfWork, IAppCache cache, IOptionsMonitor<CacheSettings> cacheSettings)
    {
        _unitOfWork = unitOfWork;
        _cache = cache;
        _cacheSettings = cacheSettings;
    }

    public async Task<Result<PaginatedList<FloorDto>>> Handle(GetFloorQuery request,
        CancellationToken cancellationToken)
    {
        var useCache = _cacheSettings.CurrentValue.UseCache;
        var data = useCache ? GetCachedData(request) : GetDatabaseData(request);

        var items = data.Data.ProjectToType<FloorDto>().ToList();

        return Result.Ok(await PaginatedList<FloorDto>.CreateAsync(items, data.Count, request.Page ?? 1, request.PageCount ?? 10));
    }

    private (IQueryable<Floor> Data, int Count) GetCachedData(GetFloorQuery request)
    {
        var cacheKey = nameof(Floor);
        var cache = _cache.GetOrAdd(cacheKey, () =>
        {
            var data = _unitOfWork.Floors!
                .FindWithSpecificationPattern(new GetFloorSpecification(all:true))
                .ToList();
            return data;
        }, DateTimeOffset.Now.AddSeconds(_cacheSettings.CurrentValue.ExpireSeconds));

        var data = _unitOfWork.Floors!.FindWithSpecificationPattern(cache.ToList(), request.Specification).Skip(((request.Page ?? 1) - 1) * (request.PageCount ?? 10)).Take(request.PageCount ?? 10);

        var count = _unitOfWork.Floors!.FindWithSpecificationPattern(cache.ToList(), request.Specification).Count();

        return (data, count);
    }

    private (IQueryable<Floor> Data, int Count) GetDatabaseData(GetFloorQuery request)
    {
        var data = _unitOfWork.Floors!.FindWithSpecificationPattern(request.Specification)
            .Skip(((request.Page ?? 1) - 1) * (request.PageCount ?? 10)).Take(request.PageCount ?? 10);
        var count = _unitOfWork.Floors!.FindWithSpecificationPattern(request.Specification).Count();

        return (data,count);
    }
}
