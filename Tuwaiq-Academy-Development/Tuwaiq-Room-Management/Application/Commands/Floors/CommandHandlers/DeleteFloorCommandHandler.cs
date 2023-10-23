using Application.Commands.Floors.Commands;
using Application.Interfaces;
using Application.Search;
using LazyCache;
using Microsoft.Extensions.Localization;
using Shared.Ids;
using Shared.Interfaces;
using Shared.Base;
using Shared.Interfaces.ValidationErrors;


namespace Application.Commands.Floors.CommandHandlers;

public class DeleteFloorCommandHandler : ICommandHandler<DeleteFloorCommand>
{
    private readonly IAppCache _cache;
    private readonly IViewRenderService _viewRenderer;
    private readonly ISearchManager _searchManager;
    private readonly IStringLocalizer<Domain.Domains.Floor> _localizer;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteFloorCommandHandler(IUnitOfWork unitOfWork, IStringLocalizer<Domain.Domains.Floor> localizer, IAppCache cache, IViewRenderService viewRenderer, ISearchManager searchManager)
    {
        _unitOfWork = unitOfWork;
        _localizer = localizer;
        _cache = cache;
        _viewRenderer = viewRenderer;
        _searchManager = searchManager;
    }


    public async Task<Result> Handle(DeleteFloorCommand request, CancellationToken cancellationToken)
    {
        if (request.Id == FloorId.Empty)
            return Result.Fail(Error.Invalid(_localizer["FloorId"]));

        var entity = await _unitOfWork.Floors!.GetByIdAsync(request.Id);
        if (entity == null) return Result.Fail(Error.NotFound(_localizer["Floor"]));

        _unitOfWork.Floors.Delete(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        _cache.Remove(nameof(Domain.Domains.Floor));

        _searchManager.DeleteFromIndex(new[] { new SearchableFloor(new () { Id = request.Id.Value }, _viewRenderer) });

        return Result.Ok();
    }
}