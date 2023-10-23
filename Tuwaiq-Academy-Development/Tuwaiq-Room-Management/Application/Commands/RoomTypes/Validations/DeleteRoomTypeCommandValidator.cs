using Application.Commands.RoomTypes.Commands;
using FluentValidation;
using Microsoft.Extensions.Localization;
using Shared.Ids;

namespace Application.Commands.RoomTypes.Validations;

public class DeleteRoomTypeCommandValidator : AbstractValidator<DeleteRoomTypeCommand>
{
    public DeleteRoomTypeCommandValidator(IStringLocalizer<DeleteRoomTypeCommand> localizer)
    {
        RuleFor(x => x.Id)
            .NotNull().WithMessage((string)localizer["IsRequired"])
            .NotEqual(RoomTypeId.Empty).WithMessage((string)localizer["IsRequired"]);
    }
}