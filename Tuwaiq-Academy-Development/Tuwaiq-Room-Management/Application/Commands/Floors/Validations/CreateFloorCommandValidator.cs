using Application.Commands.Floors.Commands;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Application.Commands.Floors.Validations;

public class CreateFloorCommandValidator : AbstractValidator<CreateFloorCommand>
{
    public CreateFloorCommandValidator(IStringLocalizer<CreateFloorCommand> localizer)
    {
        RuleFor(x => x.Name)
            .NotNull().WithMessage((string)localizer["IsRequired"])
            .MinimumLength(3).WithMessage((string)localizer["IsTooShort"])
            .MaximumLength(250).WithMessage((string)localizer["IsTooLong"]);
        
    }
}