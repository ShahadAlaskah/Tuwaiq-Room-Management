using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Shared.Ids;

namespace Infrastructure.Persistence.Ef.Converters.IdsConverters;

public class FloorIdConverter : ValueConverter<FloorId, Guid> { public FloorIdConverter() : base(c => c.Value, c => new (c)) { } }