using Application.Commands.Rooms.Commands;
using Application.Dto;
using Application.Interfaces;
using Application.Search;
using Application.Specifications;
using LazyCache;
using Mapster;
using Microsoft.Extensions.Localization;
using Shared.Ids;
using Shared.Interfaces;
using Shared.Base;
using Shared.Interfaces.ValidationErrors;

namespace Application.Commands.Rooms.CommandHandlers;

public class UpdateRoomCommandHandler : ICommandHandler<UpdateRoomCommand, RoomId>
{
    private readonly IAppCache _cache;
    private readonly IViewRenderService _viewRenderer;
    private readonly ISearchManager _searchManager;
    private readonly IStringLocalizer<Domain.Domains.Room> _localizer;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateRoomCommandHandler(IUnitOfWork unitOfWork, IStringLocalizer<Domain.Domains.Room> localizer, 
        IAppCache cache,IViewRenderService viewRenderer, ISearchManager searchManager)
    {
        _unitOfWork = unitOfWork;
        _localizer = localizer;
        _cache = cache;
        _viewRenderer = viewRenderer;
        _searchManager = searchManager;
    }

    public async Task<Result<RoomId>> Handle(UpdateRoomCommand request,
        CancellationToken cancellationToken)
    {
        if (request.RoomId == RoomId.Empty)
            return Result.Fail<RoomId>(Error.Invalid(_localizer["RoomId"]));

        var entity = _unitOfWork.Rooms!.FindWithSpecificationPattern(new GetRoomSpecification(request.RoomId)).FirstOrDefault();
        if (entity == null) return Result.Fail<RoomId>(Error.NotFound(_localizer["Room"]));

        entity.Name = request.Name;
        entity.RoomTypeId = request.RoomTypeId;
        entity.FloorId = request.FloorId;
        entity.Capacity = request.Capacity;
        entity.Code = request.Code;
        
        _unitOfWork.Rooms.Update(entity);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        _cache.Remove(nameof(Domain.Domains.Room));
        
        var searchable = entity.Adapt<RoomDto>();
        var insert = new SearchableRoom(searchable!, _viewRenderer);
        _searchManager.AddToIndex(insert);

        return Result.Ok(entity.Id);
    }
}