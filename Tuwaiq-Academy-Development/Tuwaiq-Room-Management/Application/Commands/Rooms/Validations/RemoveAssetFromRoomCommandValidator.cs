using Application.Commands.Rooms.Commands;
using FluentValidation;
using Microsoft.Extensions.Localization;
using Shared.Ids;

namespace Application.Commands.Rooms.Validations;

public class RemoveAssetFromRoomCommandValidator : AbstractValidator<RemoveAssetFromRoomCommand>
{
    public RemoveAssetFromRoomCommandValidator(IStringLocalizer<RemoveAssetFromRoomCommand> localizer)
    {

        RuleFor(x => x.RoomId)
            .NotNull().WithMessage((string)localizer["IsRequired"])
            .NotEqual(RoomId.Empty).WithMessage((string)localizer["IsRequired"]);

        RuleFor(x => x.AssetId)
            .NotNull().WithMessage((string)localizer["IsRequired"])
            .NotEqual(AssetId.Empty).WithMessage((string)localizer["IsRequired"]);
    }
}