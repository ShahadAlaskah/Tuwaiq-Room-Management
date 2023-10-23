using Application.Commands.Rooms.Commands;
using Application.Dto;
using Application.Interfaces;
using Application.Search;
using LazyCache;
using Mapster;
using Shared.Ids;
using Shared.Interfaces;
using Shared.Base;

namespace Application.Commands.Rooms.CommandHandlers;

public class CreateRoomCommandHandler : ICommandHandler<CreateRoomCommand, RoomId>
{
    private readonly IAppCache _cache;
    private readonly IViewRenderService _viewRenderer;
    private readonly ISearchManager _searchManager;
    private readonly IUnitOfWork _unitOfWork;

    public CreateRoomCommandHandler(IUnitOfWork unitOfWork, IAppCache cache, IViewRenderService viewRenderer, ISearchManager searchManager)
    {
        _unitOfWork = unitOfWork;
        _cache = cache;
        _viewRenderer = viewRenderer;
        _searchManager = searchManager;
    }

    public async Task<Result<RoomId>> Handle(CreateRoomCommand request,
        CancellationToken cancellationToken)
    {
        var item = request.Adapt<Domain.Domains.Room>();

        await _unitOfWork.Rooms?.CreateAsync(item, cancellationToken)!;
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        _cache.Remove(nameof(Domain.Domains.Room));


        var searchable = item.Adapt<RoomDto>();

        var insert = new SearchableRoom(searchable!, _viewRenderer);
        _searchManager.AddToIndex(insert);


        return Result.Ok(item.Id);
    }
}