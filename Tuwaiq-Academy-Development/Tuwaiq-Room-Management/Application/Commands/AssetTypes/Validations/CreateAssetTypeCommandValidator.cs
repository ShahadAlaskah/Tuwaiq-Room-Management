using Application.Commands.AssetTypes.Commands;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Application.Commands.AssetTypes.Validations;

public class CreateAssetTypeCommandValidator : AbstractValidator<CreateAssetTypeCommand>
{
    public CreateAssetTypeCommandValidator(IStringLocalizer<CreateAssetTypeCommand> localizer)
    {
        RuleFor(x => x.Name)
            .NotNull().WithMessage((string)localizer["IsRequired"])
            .MinimumLength(3).WithMessage((string)localizer["IsTooShort"])
            .MaximumLength(250).WithMessage((string)localizer["IsTooLong"]);
        
         RuleFor(x => x.Icon)
             .MinimumLength(3).WithMessage((string)localizer["IsTooShort"])
             .MaximumLength(250).WithMessage((string)localizer["IsTooLong"]);
        
    }
}