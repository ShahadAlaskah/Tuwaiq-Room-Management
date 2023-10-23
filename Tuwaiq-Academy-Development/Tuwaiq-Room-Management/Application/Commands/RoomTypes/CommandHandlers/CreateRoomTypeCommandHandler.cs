using Application.Commands.RoomTypes.Commands;
using Application.Dto;
using Application.Interfaces;
using Application.Search;
using Application.Specifications;
using LazyCache;
using Mapster;
using Microsoft.Extensions.Localization;
using Shared.Base;
using Shared.Ids;
using Shared.Interfaces;
using Shared.Interfaces.ValidationErrors;

namespace Application.Commands.RoomTypes.CommandHandlers;

public class CreateRoomTypeCommandHandler : ICommandHandler<CreateRoomTypeCommand, RoomTypeId>
{
    private readonly IAppCache _cache;
    private readonly IViewRenderService _viewRenderer;
    private readonly ISearchManager _searchManager;
    private readonly IUnitOfWork _unitOfWork;

    public CreateRoomTypeCommandHandler(IUnitOfWork unitOfWork, IAppCache cache, IViewRenderService viewRenderer, ISearchManager searchManager)
    {
        _unitOfWork = unitOfWork;
        _cache = cache;
        _viewRenderer = viewRenderer;
        _searchManager = searchManager;
    }

    public async Task<Result<RoomTypeId>> Handle(CreateRoomTypeCommand request,
        CancellationToken cancellationToken)
    {
        var item = request.Adapt<Domain.Domains.RoomType>();

        await _unitOfWork.RoomTypes?.CreateAsync(item, cancellationToken)!;
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        _cache.Remove(nameof(Domain.Domains.RoomType));


        var searchable = item.Adapt<RoomTypeDto>();

        var insert = new SearchableRoomType(searchable!, _viewRenderer);
        _searchManager.AddToIndex(insert);


        return Result.Ok(item.Id);
    }
}