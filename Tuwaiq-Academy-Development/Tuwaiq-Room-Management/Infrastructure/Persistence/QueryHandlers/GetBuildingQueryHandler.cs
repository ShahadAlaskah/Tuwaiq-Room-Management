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

public class GetBuildingQueryHandler : IQueryHandler<GetBuildingQuery, PaginatedList<BuildingDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAppCache _cache;

    public GetBuildingQueryHandler(IUnitOfWork unitOfWork, IAppCache cache)
    {
        _unitOfWork = unitOfWork;
        _cache = cache;
    }

    public async Task<Result<PaginatedList<BuildingDto>>> Handle(GetBuildingQuery request,
        CancellationToken cancellationToken)
    {
        var result = _cache.GetOrAdd(nameof(Domain.Domains.Building), cache =>
        {
            cache.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10);
            return _unitOfWork.Buildings!.FindWithSpecificationPattern(new GetBuildingSpecification()).ToList();
        });

        var count = _unitOfWork.Buildings!.FindWithSpecificationPattern(result, request.Specification).Count();
        var items = _unitOfWork.Buildings!.FindWithSpecificationPattern(result, request.Specification)
            .Skip(((request.Page ?? 1) - 1) * (request.PageCount ?? 10)).Take(request.PageCount ?? 10).ToList();

        var buildingDtos = items.AsQueryable().ProjectToType<BuildingDto>().ToList();
        var paginatedList = await PaginatedList<BuildingDto>.CreateAsync(buildingDtos, count, request.Page ?? 1, request.PageCount ?? 10);
        return Result.Ok<PaginatedList<BuildingDto>>(paginatedList);
    }
}