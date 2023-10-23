using Shared.Ids;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Persistence.Ef.Converters.IdsConverters;

public class AuditViewIdConverter : ValueConverter<AuditViewId, Guid> { public AuditViewIdConverter() : base(c => c.Value, c => new (c)) { } }