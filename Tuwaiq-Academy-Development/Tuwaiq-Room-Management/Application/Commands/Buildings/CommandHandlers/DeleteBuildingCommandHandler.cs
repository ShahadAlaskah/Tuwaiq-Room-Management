using Application.Commands.Buildings.Commands;
using Application.Interfaces;
using Application.Search;
using LazyCache;
using Microsoft.Extensions.Localization;
using Shared.Ids;
using Shared.Interfaces;
using Shared.Base;
using Shared.Interfaces.ValidationErrors;


namespace Application.Commands.Buildings.CommandHandlers;

public class DeleteBuildingCommandHandler : ICommandHandler<DeleteBuildingCommand>
{
    private readonly IAppCache _cache;
    private readonly IViewRenderService _viewRenderer;
    private readonly ISearchManager _searchManager;
    private readonly IStringLocalizer<Domain.Domains.Building> _localizer;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteBuildingCommandHandler(IUnitOfWork unitOfWork, IStringLocalizer<Domain.Domains.Building> localizer, IAppCache cache, IViewRenderService viewRenderer, ISearchManager searchManager)
    {
        _unitOfWork = unitOfWork;
        _localizer = localizer;
        _cache = cache;
        _viewRenderer = viewRenderer;
        _searchManager = searchManager;
    }


    public async Task<Result> Handle(DeleteBuildingCommand request, CancellationToken cancellationToken)
    {
        if (request.Id == BuildingId.Empty)
            return Result.Fail(Error.Invalid(_localizer["BuildingId"]));

        var entity = await _unitOfWork.Buildings!.GetByIdAsync(request.Id);
        if (entity == null) return Result.Fail(Error.NotFound(_localizer["Building"]));

        _unitOfWork.Buildings.Delete(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        _cache.Remove(nameof(Domain.Domains.Building));

        _searchManager.DeleteFromIndex(new[] { new SearchableBuilding(new () { Id = request.Id.Value }, _viewRenderer) });

        return Result.Ok();
    }
}