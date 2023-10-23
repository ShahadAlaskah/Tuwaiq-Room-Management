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

public class GetRoomTypeQueryHandler : IQueryHandler<GetRoomTypeQuery, PaginatedList<RoomTypeDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAppCache _cache;

    public GetRoomTypeQueryHandler(IUnitOfWork unitOfWork,IAppCache cache)
    {
        _unitOfWork = unitOfWork;
        _cache = cache;
    }

    public async Task<Result<PaginatedList<RoomTypeDto>>> Handle(GetRoomTypeQuery request,
        CancellationToken cancellationToken)
    {
       
        var result = _cache.GetOrAdd(nameof(Domain.Domains.RoomType), cache =>
        {
            cache.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10);
            return _unitOfWork.RoomTypes!.FindWithSpecificationPattern(new GetRoomTypeSpecification()).ToList();
        });

        var count = _unitOfWork.RoomTypes!.FindWithSpecificationPattern(result,request.Specification).Count();
        var items = _unitOfWork.RoomTypes!.FindWithSpecificationPattern(result,request.Specification).Skip(((request.Page ?? 1) - 1) * (request.PageCount ?? 10)).Take(request.PageCount ?? 10).ToList();

        var roomTypeDtos = items.AsQueryable().ProjectToType<RoomTypeDto>().ToList();
        var paginatedList = await PaginatedList<RoomTypeDto>.CreateAsync(roomTypeDtos, count, request.Page ?? 1, request.PageCount ?? 10);
        return Result.Ok<PaginatedList<RoomTypeDto>>(paginatedList);
    }
}