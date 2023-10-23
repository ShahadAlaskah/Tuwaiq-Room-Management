using Application.Commands.RoomTypes.Commands;
using Application.Dto;
using Application.Interfaces;
using Application.Search;
using Application.Specifications;
using LazyCache;
using Mapster;
using Microsoft.Extensions.Localization;
using Shared.Ids;
using Shared.Interfaces;
using Shared.Interfaces.ValidationErrors;
using Shared.Base;

namespace Application.Commands.RoomTypes.CommandHandlers;

public class UpdateRoomTypeCommandHandler : ICommandHandler<UpdateRoomTypeCommand, RoomTypeId>
{
    private readonly IAppCache _cache;
    private readonly IViewRenderService _viewRenderer;
    private readonly ISearchManager _searchManager;
    private readonly IStringLocalizer<Domain.Domains.RoomType> _localizer;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateRoomTypeCommandHandler(IUnitOfWork unitOfWork, IStringLocalizer<Domain.Domains.RoomType> localizer, 
        IAppCache cache,IViewRenderService viewRenderer, ISearchManager searchManager)
    {
        _unitOfWork = unitOfWork;
        _localizer = localizer;
        _cache = cache;
        _viewRenderer = viewRenderer;
        _searchManager = searchManager;
    }

    public async Task<Result<RoomTypeId>> Handle(UpdateRoomTypeCommand request,
        CancellationToken cancellationToken)
    {
        if (request.Id == RoomTypeId.Empty)
            return Result.Fail<RoomTypeId>(Error.Invalid(_localizer["RoomTypeId"]));

        var entity = _unitOfWork.RoomTypes!.FindWithSpecificationPattern(new GetRoomTypeSpecification(request.Id)).FirstOrDefault();
        if (entity == null) return Result.Fail<RoomTypeId>(Error.NotFound(_localizer["RoomType"]));

        entity.Name = request.Name;
        entity.RoomTypeEnum = request.RoomTypeEnum;

        _unitOfWork.RoomTypes.Update(entity);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        _cache.Remove(nameof(Domain.Domains.RoomType));
        
        var searchable = entity.Adapt<RoomTypeDto>();
        var insert = new SearchableRoomType(searchable!, _viewRenderer);
        _searchManager.AddToIndex(insert);

        return Result.Ok(entity.Id);
    }
}