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
using Shared.Models;
using Shared.Settings;

namespace Infrastructure.Persistence.QueryHandlers;

public class GetLookupQueryHandler : IQueryHandler<GetLookupQuery, PaginatedList<LookupDictionary>>
{
    private readonly IAppCache _cache;
    private readonly IUnitOfWork _unitOfWork;

    public GetLookupQueryHandler(IUnitOfWork unitOfWork, IAppCache cache)
    {
        _unitOfWork = unitOfWork;
        _cache = cache;
    }

    public async Task<Result<PaginatedList<LookupDictionary>>> Handle(GetLookupQuery request, CancellationToken cancellationToken)
    {
        var resultList = new List<LookupDictionary>();

        if (request.Entity == typeof(Building))
        {
            var result = _cache.GetOrAdd(nameof(Building), cache =>
            {
                cache.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10);
                return _unitOfWork.Buildings!.FindWithSpecificationPattern(new GetBuildingSpecification()).ToList();
            });
            resultList = result.Skip(((request.Page ?? 1) - 1) * (request.PageCount ?? 10)).Take(request.PageCount ?? 10).Select(s =>
                new LookupDictionary
                {
                    id = s.Id.Value.ToString(),
                    text = s.Code + " : " + s.Name
                }).ToList();
        }

        if (request.Entity == typeof(Room))
        {
            var result = _cache.GetOrAdd(nameof(Room), cache =>
            {
                cache.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10);
                return _unitOfWork.Rooms!.FindWithSpecificationPattern(new GetRoomSpecification()).ToList();
            });
            resultList = result.Skip(((request.Page ?? 1) - 1) * (request.PageCount ?? 10)).Take(request.PageCount ?? 10).Select(s =>
                new LookupDictionary
                {
                    id = s.Id.Value.ToString(),
                    text = s.Code + " : " + s.Name
                }).ToList();
        }

        if (request.Entity == typeof(RoomType))
        {
            var result = _cache.GetOrAdd(nameof(RoomType), cache =>
            {
                cache.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10);
                return _unitOfWork.RoomTypes!.FindWithSpecificationPattern(new GetRoomTypeSpecification()).ToList();
            });
            resultList = result.Skip(((request.Page ?? 1) - 1) * (request.PageCount ?? 10)).Take(request.PageCount ?? 10).Select(s =>
                new LookupDictionary
                {
                    id = s.Id.Value.ToString(),
                    text = s.Name
                }).ToList();
        }

        if (request.Entity == typeof(Floor))
        {
            var result = _cache.GetOrAdd(nameof(Floor), cache =>
            {
                cache.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10);
                return _unitOfWork.Floors!.FindWithSpecificationPattern(new GetFloorSpecification()).ToList();
            });
            
            resultList = result.Skip(((request.Page ?? 1) - 1) * (request.PageCount ?? 10)).Take(request.PageCount ?? 10).Select(s =>
                new LookupDictionary
                {
                    id = s.Id.Value.ToString(),
                    text = s.Name
                }).ToList();
        }

        if (request.Entity == typeof(AssetType))
        {
            var result = _cache.GetOrAdd(nameof(AssetType), cache =>
            {
                cache.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10);
                return _unitOfWork.AssetTypes!.FindWithSpecificationPattern(new GetAssetTypeSpecification()).ToList();
            });
            
            resultList = result.Skip(((request.Page ?? 1) - 1) * (request.PageCount ?? 10)).Take(request.PageCount ?? 10).Select(s =>
                new LookupDictionary
                {
                    id = s.Id.Value.ToString(),
                    text = s.Name
                }).ToList();
        }

        return Result.Ok(await PaginatedList<LookupDictionary>.CreateAsync(resultList, resultList.Count, request.Page ?? 1, request.PageCount ?? 10));
    }
}