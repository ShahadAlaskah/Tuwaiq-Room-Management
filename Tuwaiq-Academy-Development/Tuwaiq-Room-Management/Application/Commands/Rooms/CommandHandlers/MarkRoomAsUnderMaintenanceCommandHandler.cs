using Application.Commands.Rooms.Commands;
using Application.Interfaces;
using Microsoft.Extensions.Localization;
using Shared.Ids;
using Shared.Interfaces;
using Shared.Base;
using Shared.Interfaces.ValidationErrors;

namespace Application.Commands.Rooms.CommandHandlers;

public class MarkRoomAsUnderMaintenanceCommandHandler : ICommandHandler<MarkRoomAsUnderMaintenanceCommand>
{
    private readonly IStringLocalizer<Domain.Domains.Room> _localizer;
    private readonly IUnitOfWork _unitOfWork;

    public MarkRoomAsUnderMaintenanceCommandHandler(IUnitOfWork unitOfWork, IStringLocalizer<Domain.Domains.Room> localizer)
    {
        _unitOfWork = unitOfWork;
        _localizer = localizer;
    }

    public async Task<Result> Handle(MarkRoomAsUnderMaintenanceCommand request, CancellationToken cancellationToken)
    {
        if (request.RoomId == RoomId.Empty)
            return Result.Fail(Error.Invalid(_localizer["RoomId"]));

        var entity = await _unitOfWork.Rooms!.GetByIdAsync(request.RoomId);
        if (entity == null) return Result.Fail(Error.NotFound(_localizer["Room"]));

        entity.MarkAsUnderMaintenance();

        _unitOfWork.Rooms.Update(entity);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Ok();
    }
}