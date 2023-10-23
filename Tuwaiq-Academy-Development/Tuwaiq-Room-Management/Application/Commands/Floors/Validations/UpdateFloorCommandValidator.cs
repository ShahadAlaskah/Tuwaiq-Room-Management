using Application.Commands.Floors.Commands;
using FluentValidation;
using Microsoft.Extensions.Localization;
using Shared.Ids;

namespace Application.Commands.Floors.Validations;

public class UpdateFloorCommandValidator : AbstractValidator<UpdateFloorCommand>
{
    public UpdateFloorCommandValidator(IStringLocalizer<UpdateFloorCommand> localizer)
    {
        RuleFor(x => x.Id)
            .NotNull().WithMessage((string)localizer["IsRequired"])
            .NotEqual(FloorId.Empty).WithMessage((string)localizer["IsRequired"]);

        RuleFor(x => x.Name)
            .NotNull().WithMessage((string)localizer["IsRequired"])
            .MinimumLength(3).WithMessage((string)localizer["IsTooShort"])
            .MaximumLength(250).WithMessage((string)localizer["IsTooLong"]);
        
    }
}