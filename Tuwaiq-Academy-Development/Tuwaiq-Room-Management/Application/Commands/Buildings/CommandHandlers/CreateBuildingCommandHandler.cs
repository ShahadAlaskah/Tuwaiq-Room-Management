using Application.Commands.Buildings.Commands;
using Application.Dto;
using Application.Interfaces;
using Application.Search;
using LazyCache;
using Mapster;
using Shared.Ids;
using Shared.Interfaces;
using Shared.Base;


namespace Application.Commands.Buildings.CommandHandlers;

public class CreateBuildingCommandHandler : ICommandHandler<CreateBuildingCommand, BuildingId>
{
    private readonly IAppCache _cache;
    private readonly IViewRenderService _viewRenderer;
    private readonly ISearchManager _searchManager;
    private readonly IUnitOfWork _unitOfWork;

    public CreateBuildingCommandHandler(IUnitOfWork unitOfWork, IAppCache cache, IViewRenderService viewRenderer, ISearchManager searchManager)
    {
        _unitOfWork = unitOfWork;
        _cache = cache;
        _viewRenderer = viewRenderer;
        _searchManager = searchManager;
    }

    public async Task<Result<BuildingId>> Handle(CreateBuildingCommand request,
        CancellationToken cancellationToken)
    {
        var item = request.Adapt<Domain.Domains.Building>();

        await _unitOfWork.Buildings?.CreateAsync(item, cancellationToken)!;
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        _cache.Remove(nameof(Domain.Domains.Building));


        var searchable = item.Adapt<BuildingDto>();

        var insert = new SearchableBuilding(searchable!, _viewRenderer);
        _searchManager.AddToIndex(insert);


        return Result.Ok(item.Id);
    }
}