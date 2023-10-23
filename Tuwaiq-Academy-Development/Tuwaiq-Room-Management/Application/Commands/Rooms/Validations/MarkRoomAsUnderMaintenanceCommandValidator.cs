using Application.Commands.Rooms.Commands;
using FluentValidation;
using Microsoft.Extensions.Localization;
using Shared.Ids;

namespace Application.Commands.Rooms.Validations;

public class MarkRoomAsUnderMaintenanceCommandValidator : AbstractValidator<MarkRoomAsUnderMaintenanceCommand>
{
    public MarkRoomAsUnderMaintenanceCommandValidator(IStringLocalizer<MarkRoomAsUnderMaintenanceCommand> localizer)
    {
        RuleFor(x => x.RoomId)
            .NotNull().WithMessage((string)localizer["IsRequired"])
            .NotEqual(RoomId.Empty).WithMessage((string)localizer["IsRequired"]);
    }
}