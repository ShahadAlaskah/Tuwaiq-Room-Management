using Shared.Ids;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Persistence.Ef.Converters.IdsConverters;

public class AuditIdConverter : ValueConverter<AuditId, Guid> { public AuditIdConverter() : base(c => c.Value, c => new (c)) { } }