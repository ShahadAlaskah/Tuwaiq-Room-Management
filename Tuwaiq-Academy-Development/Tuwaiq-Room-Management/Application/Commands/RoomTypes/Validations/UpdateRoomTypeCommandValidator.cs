using Application.Commands.RoomTypes.Commands;
using FluentValidation;
using Microsoft.Extensions.Localization;
using Shared.Ids;

namespace Application.Commands.RoomTypes.Validations;

public class UpdateRoomTypeCommandValidator : AbstractValidator<UpdateRoomTypeCommand>
{
    public UpdateRoomTypeCommandValidator(IStringLocalizer<UpdateRoomTypeCommand> localizer)
    {
        RuleFor(x => x.Id)
            .NotNull().WithMessage((string)localizer["IsRequired"])
            .NotEqual(RoomTypeId.Empty).WithMessage((string)localizer["IsRequired"]);

        RuleFor(x => x.Name)
            .NotNull().WithMessage((string)localizer["IsRequired"])
            .MinimumLength(3).WithMessage((string)localizer["IsTooShort"])
            .MaximumLength(250).WithMessage((string)localizer["IsTooLong"]);
        
        RuleFor(x => x.RoomTypeEnum)
            .NotNull().WithMessage((string)localizer["IsRequired"]);
        
    }
}