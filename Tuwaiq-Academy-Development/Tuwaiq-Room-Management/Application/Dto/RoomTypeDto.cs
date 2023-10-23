using Domain.Domains;
using Domain.Enums;
using Shared.Domain;

namespace Application.Dto;

public class RoomTypeDto : BaseDto<RoomTypeDto, RoomType>
{
    public Guid Id { get; set; }

    public RoomTypeEnum RoomTypeEnum { get; set; }
    public string Name { get; set; }
}