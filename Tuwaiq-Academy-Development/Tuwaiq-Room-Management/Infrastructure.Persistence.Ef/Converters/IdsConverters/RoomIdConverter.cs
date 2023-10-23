using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Shared.Ids;

namespace Infrastructure.Persistence.Ef.Converters.IdsConverters;

public class RoomIdConverter : ValueConverter<RoomId, Guid> { public RoomIdConverter() : base(c => c.Value, c => new (c)) { } }