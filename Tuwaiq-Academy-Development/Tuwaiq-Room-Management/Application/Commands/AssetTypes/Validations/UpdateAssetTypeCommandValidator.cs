using Application.Commands.AssetTypes.Commands;
using FluentValidation;
using Microsoft.Extensions.Localization;
using Shared.Ids;

namespace Application.Commands.AssetTypes.Validations;

public class UpdateAssetTypeCommandValidator : AbstractValidator<UpdateAssetTypeCommand>
{
    public UpdateAssetTypeCommandValidator(IStringLocalizer<UpdateAssetTypeCommand> localizer)
    {
        RuleFor(x => x.Id)
            .NotNull().WithMessage((string)localizer["IsRequired"])
            .NotEqual(AssetTypeId.Empty).WithMessage((string)localizer["IsRequired"]);

        RuleFor(x => x.Name)
            .NotNull().WithMessage((string)localizer["IsRequired"])
            .MinimumLength(3).WithMessage((string)localizer["IsTooShort"])
            .MaximumLength(250).WithMessage((string)localizer["IsTooLong"]);
        
        RuleFor(x => x.Icon)
            .MinimumLength(3).WithMessage((string)localizer["IsTooShort"])
            .MaximumLength(250).WithMessage((string)localizer["IsTooLong"]);

    }
}