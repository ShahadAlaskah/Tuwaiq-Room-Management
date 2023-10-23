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

public class GetRoomQueryHandler : IQueryHandler<GetRoomQuery, PaginatedList<RoomDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAppCache _cache;

    public GetRoomQueryHandler(IUnitOfWork unitOfWork,IAppCache cache)
    {
        _unitOfWork = unitOfWork;
        _cache = cache;
    }

    public async Task<Result<PaginatedList<RoomDto>>> Handle(GetRoomQuery request,
        CancellationToken cancellationToken)
    {
       
        var result = _cache.GetOrAdd(nameof(Domain.Domains.Room), cache =>
        {
            cache.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10);
            return _unitOfWork.Rooms!.FindWithSpecificationPattern(new GetRoomSpecification()).ToList();
        });

        var count = _unitOfWork.Rooms!.FindWithSpecificationPattern(result,request.Specification).Count();
        var items = _unitOfWork.Rooms!.FindWithSpecificationPattern(result,request.Specification).Skip(((request.Page ?? 1) - 1) * (request.PageCount ?? 10)).Take(request.PageCount ?? 10).ToList();

        var roomDtos = items.AsQueryable().ProjectToType<RoomDto>().ToList();
        var paginatedList = await PaginatedList<RoomDto>.CreateAsync(roomDtos, count, request.Page ?? 1, request.PageCount ?? 10);
        return Result.Ok<PaginatedList<RoomDto>>(paginatedList);
    }
}