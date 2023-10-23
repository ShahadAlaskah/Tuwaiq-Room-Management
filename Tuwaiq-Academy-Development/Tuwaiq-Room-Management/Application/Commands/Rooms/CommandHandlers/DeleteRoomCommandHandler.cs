using Application.Commands.Rooms.Commands;
using Application.Interfaces;
using Application.Search;
using LazyCache;
using Microsoft.Extensions.Localization;
using Shared.Ids;
using Shared.Interfaces;
using Shared.Base;
using Shared.Interfaces.ValidationErrors;

namespace Application.Commands.Rooms.CommandHandlers;

public class DeleteRoomCommandHandler : ICommandHandler<DeleteRoomCommand>
{
    private readonly IAppCache _cache;
    private readonly IViewRenderService _viewRenderer;
    private readonly ISearchManager _searchManager;
    private readonly IStringLocalizer<Domain.Domains.Room> _localizer;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteRoomCommandHandler(IUnitOfWork unitOfWork, IStringLocalizer<Domain.Domains.Room> localizer, IAppCache cache, IViewRenderService viewRenderer, ISearchManager searchManager)
    {
        _unitOfWork = unitOfWork;
        _localizer = localizer;
        _cache = cache;
        _viewRenderer = viewRenderer;
        _searchManager = searchManager;
    }


    public async Task<Result> Handle(DeleteRoomCommand request, CancellationToken cancellationToken)
    {
        if (request.RoomId == RoomId.Empty)
            return Result.Fail(Error.Invalid(_localizer["RoomId"]));

        var entity = await _unitOfWork.Rooms!.GetByIdAsync(request.RoomId);
        if (entity == null) return Result.Fail(Error.NotFound(_localizer["Room"]));

        _unitOfWork.Rooms.Delete(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        _cache.Remove(nameof(Domain.Domains.Room));

        _searchManager.DeleteFromIndex(new[] { new SearchableRoom(new () { Id = request.RoomId.Value }, _viewRenderer) });

        return Result.Ok();
    }
}