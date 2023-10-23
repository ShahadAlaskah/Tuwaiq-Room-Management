using Application.Commands.RoomTypes.Commands;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Application.Commands.RoomTypes.Validations;

public class CreateRoomTypeCommandValidator : AbstractValidator<CreateRoomTypeCommand>
{
    public CreateRoomTypeCommandValidator(IStringLocalizer<CreateRoomTypeCommand> localizer)
    {
        RuleFor(x => x.Name)
            .NotNull().WithMessage((string)localizer["IsRequired"])
            .MinimumLength(3).WithMessage((string)localizer["IsTooShort"])
            .MaximumLength(250).WithMessage((string)localizer["IsTooLong"]);
        
        RuleFor(x => x.RoomTypeEnum)
            .NotNull().WithMessage((string)localizer["IsRequired"]);
    }
}