using Application.Commands.RoomTypes.Commands;
using Application.Interfaces;
using Application.Search;
using LazyCache;
using Microsoft.Extensions.Localization;
using Shared.Ids;
using Shared.Interfaces;
using Shared.Base;
using Shared.Interfaces.ValidationErrors;

namespace Application.Commands.RoomTypes.CommandHandlers;

public class DeleteRoomTypeCommandHandler : ICommandHandler<DeleteRoomTypeCommand>
{
    private readonly IAppCache _cache;
    private readonly IViewRenderService _viewRenderer;
    private readonly ISearchManager _searchManager;
    private readonly IStringLocalizer<Domain.Domains.RoomType> _localizer;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteRoomTypeCommandHandler(IUnitOfWork unitOfWork, IStringLocalizer<Domain.Domains.RoomType> localizer, IAppCache cache, IViewRenderService viewRenderer, ISearchManager searchManager)
    {
        _unitOfWork = unitOfWork;
        _localizer = localizer;
        _cache = cache;
        _viewRenderer = viewRenderer;
        _searchManager = searchManager;
    }


    public async Task<Result> Handle(DeleteRoomTypeCommand request, CancellationToken cancellationToken)
    {
        if (request.Id == RoomTypeId.Empty)
            return Result.Fail(Error.Invalid(_localizer["RoomTypeId"]));

        var entity = await _unitOfWork.RoomTypes!.GetByIdAsync(request.Id);
        if (entity == null) return Result.Fail(Error.NotFound(_localizer["RoomType"]));

        _unitOfWork.RoomTypes.Delete(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        _cache.Remove(nameof(Domain.Domains.RoomType));

        _searchManager.DeleteFromIndex(new[] { new SearchableRoomType(new () { Id = request.Id.Value }, _viewRenderer) });

        return Result.Ok();
    }
}