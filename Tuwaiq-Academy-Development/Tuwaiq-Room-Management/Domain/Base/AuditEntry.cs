// using System.ComponentModel.DataAnnotations;
// using System.ComponentModel.DataAnnotations.Schema;
// using API.Domain.Shared;
// using Microsoft.EntityFrameworkCore.ChangeTracking;
//

using Newtonsoft.Json;
using Shared.Domain;
using Shared.Enums;
using Shared.Ids;

namespace Domain.Base;

public class AuditEntry<T, TPt> : BaseEntity where T : class where TPt : class
{
    public AuditEntry(T entry)
    {
        Entry = entry;
    }

    public T Entry { get; }
    public UserId? UserId { get; set; } = null!;
    public string? ClientId { get; set; } = null!;
    public string TableName { get; set; } = null!;
    public Dictionary<string, object> KeyValues { get; } = new();
    public Dictionary<string, object> OldValues { get; } = new();
    public Dictionary<string, object> NewValues { get; } = new();
    public List<TPt> TemporaryProperties { get; } = new();
    public AuditType AuditType { get; set; }
    public List<string> ChangedColumns { get; } = new();
    public bool HasTemporaryProperties => TemporaryProperties.Any();

    public Audit ToAudit()
    {
        var audit = new Audit
        {
            UserId = UserId,
            ClientId = ClientId,
            Type = AuditType.ToString(),
            TableName = TableName,
            DateTime = DateTime.UtcNow,
            PrimaryKey = JsonConvert.SerializeObject(KeyValues),
            OldValues = OldValues.Count == 0 ? null : JsonConvert.SerializeObject(OldValues),
            NewValues = NewValues.Count == 0 ? null : JsonConvert.SerializeObject(NewValues),
            AffectedColumns = ChangedColumns.Count == 0 ? null : JsonConvert.SerializeObject(ChangedColumns)
        };
        return audit;
    }
}