using Shared.Domain;
using Shared.Enums;
using Shared.Ids;

namespace Domain.Base;

public class AuditView : IBaseEntity
{
    public AuditViewId Id { get; set; } = new(Guid.NewGuid());
    public UserId? UserId { get; set; } = null!;
    public AuditViewType Type { get; set; }
    public Dictionary<string, object>? Args { get; set; }
    public DateTime DateTime { get; set; }
    public string? Description { get; set; }
    public string[]? Tables { get; set; }
}