using Application.Commands.Floors.Commands;
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


namespace Application.Commands.Floors.CommandHandlers;

public class UpdateFloorCommandHandler : ICommandHandler<UpdateFloorCommand, FloorId>
{
    private readonly IAppCache _cache;
    private readonly IViewRenderService _viewRenderer;
    private readonly ISearchManager _searchManager;
    private readonly IStringLocalizer<Domain.Domains.Floor> _localizer;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateFloorCommandHandler(IUnitOfWork unitOfWork, IStringLocalizer<Domain.Domains.Floor> localizer, 
        IAppCache cache,IViewRenderService viewRenderer, ISearchManager searchManager)
    {
        _unitOfWork = unitOfWork;
        _localizer = localizer;
        _cache = cache;
        _viewRenderer = viewRenderer;
        _searchManager = searchManager;
    }

    public async Task<Result<FloorId>> Handle(UpdateFloorCommand request,
        CancellationToken cancellationToken)
    {
        if (request.Id == FloorId.Empty)
            return Result.Fail<FloorId>(Error.Invalid(_localizer["FloorId"]));

        var entity = _unitOfWork.Floors!.FindWithSpecificationPattern(new GetFloorSpecification(request.Id)).FirstOrDefault();
        if (entity == null) return Result.Fail<FloorId>(Error.NotFound(_localizer["Floor"]));

        entity.Name = request.Name;

        _unitOfWork.Floors.Update(entity);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        _cache.Remove(nameof(Domain.Domains.Floor));
        
        var searchable = entity.Adapt<FloorDto>();
        var insert = new SearchableFloor(searchable!, _viewRenderer);
        _searchManager.AddToIndex(insert);

        return Result.Ok(entity.Id);
    }
}