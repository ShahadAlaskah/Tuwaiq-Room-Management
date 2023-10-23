using Application.Commands.Rooms.Commands;
using FluentValidation;
using Microsoft.Extensions.Localization;
using Shared.Ids;

namespace Application.Commands.Rooms.Validations;

public class UpdateRoomCommandValidator : AbstractValidator<UpdateRoomCommand>
{
    public UpdateRoomCommandValidator(IStringLocalizer<UpdateRoomCommand> localizer)
    {
        RuleFor(x => x.RoomId)
            .NotNull().WithMessage((string)localizer["IsRequired"])
            .NotEqual(RoomId.Empty).WithMessage((string)localizer["IsRequired"]);

        RuleFor(x => x.Name)
            .NotNull().WithMessage((string)localizer["IsRequired"])
            .MinimumLength(3).WithMessage((string)localizer["IsTooShort"])
            .MaximumLength(250).WithMessage((string)localizer["IsTooLong"]);
        
        RuleFor(x => x.Code)
            .NotNull().WithMessage((string)localizer["IsRequired"])
            .MinimumLength(1).WithMessage((string)localizer["IsTooShort"])
            .MaximumLength(250).WithMessage((string)localizer["IsTooLong"]);
        
        RuleFor(x => x.FloorId)
            .NotNull().WithMessage((string)localizer["IsRequired"])
            .NotEqual(FloorId.Empty).WithMessage((string)localizer["IsRequired"]);

        RuleFor(x => x.BuildingId)
            .NotNull().WithMessage((string)localizer["IsRequired"])
            .NotEqual(BuildingId.Empty).WithMessage((string)localizer["IsRequired"]);
        
        RuleFor(s=>s.Capacity)
            .NotNull().WithMessage((string)localizer["IsRequired"])
            .GreaterThan(0).WithMessage((string)localizer["IsTooShort"])
            .LessThan(10000).WithMessage((string)localizer["IsTooLong"]);

    }
}