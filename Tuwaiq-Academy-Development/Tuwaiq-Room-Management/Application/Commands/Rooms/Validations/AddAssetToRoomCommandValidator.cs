using Application.Commands.Rooms.Commands;
using FluentValidation;
using Microsoft.Extensions.Localization;
using Shared.Ids;

namespace Application.Commands.Rooms.Validations;

public class AddAssetToRoomCommandValidator : AbstractValidator<AddAssetToRoomCommand>
{
    public AddAssetToRoomCommandValidator(IStringLocalizer<AddAssetToRoomCommand> localizer)
    {

        RuleFor(x => x.InstalledDate)
            .NotNull().WithMessage((string)localizer["IsRequired"]);
        
        RuleFor(x => x.Code)
            .NotNull().WithMessage((string)localizer["IsRequired"])
            .MinimumLength(1).WithMessage((string)localizer["IsTooShort"])
            .MaximumLength(250).WithMessage((string)localizer["IsTooLong"]);

        RuleFor(x => x.RoomId)
            .NotNull().WithMessage((string)localizer["IsRequired"])
            .NotEqual(RoomId.Empty).WithMessage((string)localizer["IsRequired"]);

        RuleFor(x => x.AssetTypeId)
            .NotNull().WithMessage((string)localizer["IsRequired"])
            .NotEqual(AssetTypeId.Empty).WithMessage((string)localizer["IsRequired"]);
    }
}