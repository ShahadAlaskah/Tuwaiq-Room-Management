using Application.Commands.Floors.Commands;
using FluentValidation;
using Microsoft.Extensions.Localization;
using Shared.Ids;

namespace Application.Commands.Floors.Validations;

public class DeleteFloorCommandValidator : AbstractValidator<DeleteFloorCommand>
{
    public DeleteFloorCommandValidator(IStringLocalizer<DeleteFloorCommand> localizer)
    {
        RuleFor(x => x.Id)
            .NotNull().WithMessage((string)localizer["IsRequired"])
            .NotEqual(FloorId.Empty).WithMessage((string)localizer["IsRequired"]);
    }
}