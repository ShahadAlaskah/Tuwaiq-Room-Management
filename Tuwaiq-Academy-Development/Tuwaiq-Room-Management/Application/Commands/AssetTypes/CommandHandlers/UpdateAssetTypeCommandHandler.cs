using Application.Commands.AssetTypes.Commands;
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


namespace Application.Commands.AssetTypes.CommandHandlers;

public class UpdateAssetTypeCommandHandler : ICommandHandler<UpdateAssetTypeCommand, AssetTypeId>
{
    private readonly IAppCache _cache;
    private readonly IViewRenderService _viewRenderer;
    private readonly ISearchManager _searchManager;
    private readonly IStringLocalizer<Domain.Domains.AssetType> _localizer;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateAssetTypeCommandHandler(IUnitOfWork unitOfWork, IStringLocalizer<Domain.Domains.AssetType> localizer, 
        IAppCache cache,IViewRenderService viewRenderer, ISearchManager searchManager)
    {
        _unitOfWork = unitOfWork;
        _localizer = localizer;
        _cache = cache;
        _viewRenderer = viewRenderer;
        _searchManager = searchManager;
    }

    public async Task<Result<AssetTypeId>> Handle(UpdateAssetTypeCommand request,
        CancellationToken cancellationToken)
    {
        if (request.Id == AssetTypeId.Empty)
            return Result.Fail<AssetTypeId>(Error.Invalid(_localizer["AssetTypeId"]));

        var entity = _unitOfWork.AssetTypes!.FindWithSpecificationPattern(new GetAssetTypeSpecification(request.Id)).FirstOrDefault();
        if (entity == null) return Result.Fail<AssetTypeId>(Error.NotFound(_localizer["AssetType"]));

        entity.Name = request.Name;
        entity.Icon = request.Icon;

        _unitOfWork.AssetTypes.Update(entity);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        _cache.Remove(nameof(Domain.Domains.AssetType));
        
        var searchable = entity.Adapt<AssetTypeDto>();
        var insert = new SearchableAssetType(searchable!, _viewRenderer);
        _searchManager.AddToIndex(insert);

        return Result.Ok(entity.Id);
    }
}