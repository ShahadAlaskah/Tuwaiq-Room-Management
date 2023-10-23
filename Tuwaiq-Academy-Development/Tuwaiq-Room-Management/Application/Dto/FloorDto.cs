using Domain.Domains;
using Shared.Domain;

namespace Application.Dto;

public class FloorDto : BaseDto<FloorDto, Floor>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}