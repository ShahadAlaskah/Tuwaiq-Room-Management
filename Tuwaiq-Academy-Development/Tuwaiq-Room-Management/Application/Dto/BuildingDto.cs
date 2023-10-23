using Domain.Domains;
using Shared.Domain;

namespace Application.Dto;

public class BuildingDto : BaseDto<BuildingDto, Building>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
}