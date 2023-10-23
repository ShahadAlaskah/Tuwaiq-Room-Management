using Domain.Domains;
using Shared.Domain;

namespace Application.Dto;

public class AssetTypeDto : BaseDto<AssetTypeDto, AssetType>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? Icon { get; set; }
}