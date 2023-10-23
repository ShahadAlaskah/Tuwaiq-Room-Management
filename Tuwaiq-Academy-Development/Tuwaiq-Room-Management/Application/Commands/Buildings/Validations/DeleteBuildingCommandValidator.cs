using Application.Commands.Buildings.Commands;
using FluentValidation;
using Microsoft.Extensions.Localization;
using Shared.Ids;

namespace Application.Commands.Buildings.Validations;

public class DeleteBuildingCommandValidator : AbstractValidator<DeleteBuildingCommand>
{
    public DeleteBuildingCommandValidator(IStringLocalizer<DeleteBuildingCommand> localizer)
    {
        RuleFor(x => x.Id)
            .NotNull().WithMessage((string)localizer["IsRequired"])
            .NotEqual(BuildingId.Empty).WithMessage((string)localizer["IsRequired"]);
    }
}