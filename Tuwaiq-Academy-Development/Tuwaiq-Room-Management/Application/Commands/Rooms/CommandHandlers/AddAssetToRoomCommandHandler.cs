using Application.Commands.Rooms.Commands;
using Application.Interfaces;
using Microsoft.Extensions.Localization;
using Shared.Ids;
using Shared.Interfaces;
using Shared.Interfaces.ValidationErrors;
using Shared.Base;


namespace Application.Commands.Rooms.CommandHandlers;

public class AddAssetToRoomCommandHandler : ICommandHandler<AddAssetToRoomCommand, AssetId>
{
    private readonly IStringLocalizer<Domain.Domains.Room> _localizer;
    private readonly IUnitOfWork _unitOfWork;

    public AddAssetToRoomCommandHandler(IUnitOfWork unitOfWork, IStringLocalizer<Domain.Domains.Room> localizer)
    {
        _unitOfWork = unitOfWork;
        _localizer = localizer;
    }

    public async Task<Result<AssetId>> Handle(AddAssetToRoomCommand request, CancellationToken cancellationToken)
    {
        if (request.RoomId == RoomId.Empty)
            return Result.Fail<AssetId>(Error.Invalid(_localizer["RoomId"]));

        var entity = await _unitOfWork.Rooms!.GetByIdAsync(request.RoomId);
        if (entity == null) return Result.Fail<AssetId>(Error.NotFound(_localizer["Room"]));

        var assetId = entity.AddAsset(request.Code, request.AssetTypeId, request.InstalledDate);

        if (assetId.Success)
        {
            _unitOfWork.Rooms.Update(entity);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Ok(assetId.Value);
        }

        return Result.Fail<AssetId>(assetId.Error);
    }
}