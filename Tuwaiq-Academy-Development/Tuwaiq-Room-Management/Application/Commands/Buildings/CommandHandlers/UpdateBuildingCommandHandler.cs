using Application.Commands.Buildings.Commands;
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


namespace Application.Commands.Buildings.CommandHandlers;

public class UpdateBuildingCommandHandler : ICommandHandler<UpdateBuildingCommand, BuildingId>
{
    private readonly IAppCache _cache;
    private readonly IViewRenderService _viewRenderer;
    private readonly ISearchManager _searchManager;
    private readonly IStringLocalizer<Domain.Domains.Building> _localizer;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateBuildingCommandHandler(IUnitOfWork unitOfWork, IStringLocalizer<Domain.Domains.Building> localizer, 
        IAppCache cache,IViewRenderService viewRenderer, ISearchManager searchManager)
    {
        _unitOfWork = unitOfWork;
        _localizer = localizer;
        _cache = cache;
        _viewRenderer = viewRenderer;
        _searchManager = searchManager;
    }

    public async Task<Result<BuildingId>> Handle(UpdateBuildingCommand request,
        CancellationToken cancellationToken)
    {
        if (request.Id == BuildingId.Empty)
            return Result.Fail<BuildingId>(Error.Invalid(_localizer["BuildingId"]));

        var entity = _unitOfWork.Buildings!.FindWithSpecificationPattern(new GetBuildingSpecification(request.Id)).FirstOrDefault();
        if (entity == null) return Result.Fail<BuildingId>(Error.NotFound(_localizer["Building"]));

        entity.Name = request.Name;
        entity.Code = request.Code;

        _unitOfWork.Buildings.Update(entity);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        _cache.Remove(nameof(Domain.Domains.Building));
        
        var searchable = entity.Adapt<BuildingDto>();
        var insert = new SearchableBuilding(searchable!, _viewRenderer);
        _searchManager.AddToIndex(insert);

        return Result.Ok(entity.Id);
    }
}