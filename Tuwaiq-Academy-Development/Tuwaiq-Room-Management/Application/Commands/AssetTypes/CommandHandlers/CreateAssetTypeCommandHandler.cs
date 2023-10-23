using Application.Commands.AssetTypes.Commands;
using Application.Dto;
using Application.Interfaces;
using Application.Search;
using LazyCache;
using Mapster;
using Shared.Ids;
using Shared.Interfaces;
using Shared.Base;


namespace Application.Commands.AssetTypes.CommandHandlers;

public class CreateAssetTypeCommandHandler : ICommandHandler<CreateAssetTypeCommand, AssetTypeId>
{
    private readonly IAppCache _cache;
    private readonly IViewRenderService _viewRenderer;
    private readonly ISearchManager _searchManager;
    private readonly IUnitOfWork _unitOfWork;

    public CreateAssetTypeCommandHandler(IUnitOfWork unitOfWork, IAppCache cache, IViewRenderService viewRenderer, ISearchManager searchManager)
    {
        _unitOfWork = unitOfWork;
        _cache = cache;
        _viewRenderer = viewRenderer;
        _searchManager = searchManager;
    }

    public async Task<Result<AssetTypeId>> Handle(CreateAssetTypeCommand request,
        CancellationToken cancellationToken)
    {
        var item = request.Adapt<Domain.Domains.AssetType>();

        await _unitOfWork.AssetTypes?.CreateAsync(item, cancellationToken)!;
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        _cache.Remove(nameof(Domain.Domains.AssetType));


        var searchable = item.Adapt<AssetTypeDto>();

        var insert = new SearchableAssetType(searchable!, _viewRenderer);
        _searchManager.AddToIndex(insert);


        return Result.Ok(item.Id);
    }
}