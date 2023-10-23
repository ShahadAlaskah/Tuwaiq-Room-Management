using Application.Commands.AssetTypes.Commands;
using FluentValidation;
using Microsoft.Extensions.Localization;
using Shared.Ids;

namespace Application.Commands.AssetTypes.Validations;

public class DeleteAssetTypeCommandValidator : AbstractValidator<DeleteAssetTypeCommand>
{
    public DeleteAssetTypeCommandValidator(IStringLocalizer<DeleteAssetTypeCommand> localizer)
    {
        RuleFor(x => x.Id)
            .NotNull().WithMessage((string)localizer["IsRequired"])
            .NotEqual(AssetTypeId.Empty).WithMessage((string)localizer["IsRequired"]);
    }
}