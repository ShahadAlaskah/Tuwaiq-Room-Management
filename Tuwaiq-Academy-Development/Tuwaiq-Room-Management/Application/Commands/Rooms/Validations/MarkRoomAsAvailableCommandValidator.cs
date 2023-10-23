using Application.Commands.Rooms.Commands;
using FluentValidation;
using Microsoft.Extensions.Localization;
using Shared.Ids;

namespace Application.Commands.Rooms.Validations;

public class MarkRoomAsAvailableCommandValidator : AbstractValidator<MarkRoomAsAvailableCommand>
{
    public MarkRoomAsAvailableCommandValidator(IStringLocalizer<MarkRoomAsAvailableCommand> localizer)
    {
        RuleFor(x => x.RoomId)
            .NotNull().WithMessage((string)localizer["IsRequired"])
            .NotEqual(RoomId.Empty).WithMessage((string)localizer["IsRequired"]);
    }
}