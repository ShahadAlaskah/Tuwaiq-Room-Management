using Domain.Domains;
using Shared.Domain;

namespace Application.Dto;

public class AssetDto : BaseDto<AssetDto, Asset>
{
    public Guid Id { get; set; }
    public string Code { get; set; } = null!;
    public Guid? RoomId { get; set; }
    public RoomDto Room { get; set; } = null!;
    public Guid AssetTypeId { get; set; }
    public AssetTypeDto AssetType { get; set; } = null!;
    public DateOnly InstalledDate { get; set; }
    public DateTimeOffset AddedOn { get; init; }
}