using Application.Commands.Buildings.Commands;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Application.Commands.Buildings.Validations;

public class CreateBuildingCommandValidator : AbstractValidator<CreateBuildingCommand>
{
    public CreateBuildingCommandValidator(IStringLocalizer<CreateBuildingCommand> localizer)
    {
        RuleFor(x => x.Name)
            .NotNull().WithMessage((string)localizer["IsRequired"])
            .MinimumLength(3).WithMessage((string)localizer["IsTooShort"])
            .MaximumLength(250).WithMessage((string)localizer["IsTooLong"]);
        
         RuleFor(x => x.Code)
             .NotNull().WithMessage((string)localizer["IsRequired"])
             .MinimumLength(1).WithMessage((string)localizer["IsTooShort"])
             .MaximumLength(250).WithMessage((string)localizer["IsTooLong"]);
        
    }
}