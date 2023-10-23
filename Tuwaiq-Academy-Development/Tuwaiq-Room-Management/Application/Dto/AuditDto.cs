using Domain.Base;
using Shared.Domain;
using Shared.Ids;

namespace Application.Dto;

public class AuditDto : BaseDto<AuditDto, Audit>
{
    public AuditId Id { get; set; } = new(Guid.NewGuid());
    public UserId? UserId { get; set; }
    public string Type { get; set; } = null!;
    public string TableName { get; set; } = null!;
    public DateTime DateTime { get; set; }
    public string? OldValues { get; set; } = null!;
    public string? NewValues { get; set; } = null!;
    public string? AffectedColumns { get; set; } = null!;
    public string PrimaryKey { get; set; } = null!;
}