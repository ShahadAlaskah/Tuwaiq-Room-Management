using Application.Commands.Rooms.Commands;
using Application.Interfaces;
using Microsoft.Extensions.Localization;
using Shared.Ids;
using Shared.Interfaces;
using Shared.Base;
using Shared.Interfaces.ValidationErrors;

namespace Application.Commands.Rooms.CommandHandlers;

public class TransferAssetFromRoomCommandHandler : ICommandHandler<TransferAssetFromRoomCommand>
{
    private readonly IStringLocalizer<Domain.Domains.Room> _localizer;
    private readonly IUnitOfWork _unitOfWork;

    public TransferAssetFromRoomCommandHandler(IUnitOfWork unitOfWork, IStringLocalizer<Domain.Domains.Room> localizer)
    {
        _unitOfWork = unitOfWork;
        _localizer = localizer;
    }

    public async Task<Result> Handle(TransferAssetFromRoomCommand request, CancellationToken cancellationToken)
    {
        if (request.FromRoomId == RoomId.Empty)
            return Result.Fail(Error.Invalid(_localizer["FromRoomId"]));

        if (request.ToRoomId == RoomId.Empty)
            return Result.Fail(Error.Invalid(_localizer["ToRoomId"]));

        if (request.AssetId == AssetId.Empty)
            return Result.Fail(Error.Invalid(_localizer["AssetId"]));

        var entity = await _unitOfWork.Rooms!.GetByIdAsync(request.FromRoomId);
        if (entity == null) return Result.Fail(Error.NotFound(_localizer["Room"]));

        entity.TransferAsset(request.ToRoomId, request.AssetId);

        _unitOfWork.Rooms.Update(entity);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Ok();
    }
}