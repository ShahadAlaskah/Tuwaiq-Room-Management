using Domain.Enums;
using Shared.Domain;
using Shared.Ids;

namespace Domain.Domains;

public class RoomType : IBaseEntity
{
    public RoomType(string name, RoomTypeEnum roomTypeEnum)
    {
        Name = name;
        RoomTypeEnum = roomTypeEnum;
    }

    public RoomTypeId Id { get; set; } = RoomTypeId.New();
    public RoomTypeEnum RoomTypeEnum { get; set; }
    public string Name { get; set; }
}