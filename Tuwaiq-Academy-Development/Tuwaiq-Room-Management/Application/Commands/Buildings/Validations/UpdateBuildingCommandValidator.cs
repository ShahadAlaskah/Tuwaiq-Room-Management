using Application.Commands.Buildings.Commands;
using FluentValidation;
using Microsoft.Extensions.Localization;
using Shared.Ids;

namespace Application.Commands.Buildings.Validations;

public class UpdateBuildingCommandValidator : AbstractValidator<UpdateBuildingCommand>
{
    public UpdateBuildingCommandValidator(IStringLocalizer<UpdateBuildingCommand> localizer)
    {
        RuleFor(x => x.Id)
            .NotNull().WithMessage((string)localizer["IsRequired"])
            .NotEqual(BuildingId.Empty).WithMessage((string)localizer["IsRequired"]);

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