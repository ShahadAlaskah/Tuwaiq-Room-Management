using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Shared.Ids;

namespace Infrastructure.Persistence.Ef.Converters.IdsConverters;

public class RoomTypeIdConverter : ValueConverter<RoomTypeId, Guid> { public RoomTypeIdConverter() : base(c => c.Value, c => new (c)) { } }