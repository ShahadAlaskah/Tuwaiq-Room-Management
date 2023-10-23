using Application.Commands.AssetTypes.Commands;
using Application.Interfaces;
using Application.Search;
using LazyCache;
using Microsoft.Extensions.Localization;
using Shared.Ids;
using Shared.Interfaces;
using Shared.Base;
using Shared.Interfaces.ValidationErrors;


namespace Application.Commands.AssetTypes.CommandHandlers;

public class DeleteAssetTypeCommandHandler : ICommandHandler<DeleteAssetTypeCommand>
{
    private readonly IAppCache _cache;
    private readonly IViewRenderService _viewRenderer;
    private readonly ISearchManager _searchManager;
    private readonly IStringLocalizer<Domain.Domains.AssetType> _localizer;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteAssetTypeCommandHandler(IUnitOfWork unitOfWork, IStringLocalizer<Domain.Domains.AssetType> localizer, IAppCache cache, IViewRenderService viewRenderer, ISearchManager searchManager)
    {
        _unitOfWork = unitOfWork;
        _localizer = localizer;
        _cache = cache;
        _viewRenderer = viewRenderer;
        _searchManager = searchManager;
    }


    public async Task<Result> Handle(DeleteAssetTypeCommand request, CancellationToken cancellationToken)
    {
        if (request.Id == AssetTypeId.Empty)
            return Result.Fail(Error.Invalid(_localizer["AssetTypeId"]));

        var entity = await _unitOfWork.AssetTypes!.GetByIdAsync(request.Id);
        if (entity == null) return Result.Fail(Error.NotFound(_localizer["AssetType"]));

        _unitOfWork.AssetTypes.Delete(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        _cache.Remove(nameof(Domain.Domains.AssetType));

        _searchManager.DeleteFromIndex(new[] { new SearchableAssetType(new () { Id = request.Id.Value }, _viewRenderer) });

        return Result.Ok();
    }
}