using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Shared.Ids;

namespace Infrastructure.Persistence.Ef.Converters.IdsConverters;

public class BuildingIdConverter : ValueConverter<BuildingId, Guid> { public BuildingIdConverter() : base(c => c.Value, c => new (c)) { } }