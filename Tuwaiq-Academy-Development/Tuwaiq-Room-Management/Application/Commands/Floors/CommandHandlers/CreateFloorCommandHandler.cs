using Application.Commands.Floors.Commands;
using Application.Dto;
using Application.Interfaces;
using Application.Search;
using LazyCache;
using Mapster;
using Shared.Ids;
using Shared.Interfaces;
using Shared.Base;


namespace Application.Commands.Floors.CommandHandlers;

public class CreateFloorCommandHandler : ICommandHandler<CreateFloorCommand, FloorId>
{
    private readonly IAppCache _cache;
    private readonly IViewRenderService _viewRenderer;
    private readonly ISearchManager _searchManager;
    private readonly IUnitOfWork _unitOfWork;

    public CreateFloorCommandHandler(IUnitOfWork unitOfWork, IAppCache cache, IViewRenderService viewRenderer, ISearchManager searchManager)
    {
        _unitOfWork = unitOfWork;
        _cache = cache;
        _viewRenderer = viewRenderer;
        _searchManager = searchManager;
    }

    public async Task<Result<FloorId>> Handle(CreateFloorCommand request,
        CancellationToken cancellationToken)
    {
        var item = request.Adapt<Domain.Domains.Floor>();

        await _unitOfWork.Floors?.CreateAsync(item, cancellationToken)!;
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        _cache.Remove(nameof(Domain.Domains.Floor));


        var searchable = item.Adapt<FloorDto>();

        var insert = new SearchableFloor(searchable!, _viewRenderer);
        _searchManager.AddToIndex(insert);


        return Result.Ok(item.Id);
    }
}