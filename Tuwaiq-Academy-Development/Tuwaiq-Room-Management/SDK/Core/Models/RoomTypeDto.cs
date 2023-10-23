using Domain.Enums;

namespace SDK.Core.Models;

public class RoomTypeDto 
{
    public Guid Id { get; set; }

    public RoomTypeEnum RoomTypeEnum { get; set; }
    public string Name { get; set; }
}