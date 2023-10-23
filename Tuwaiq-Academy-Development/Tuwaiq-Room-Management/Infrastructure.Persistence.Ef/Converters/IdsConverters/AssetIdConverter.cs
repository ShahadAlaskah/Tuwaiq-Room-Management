using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Shared.Ids;

namespace Infrastructure.Persistence.Ef.Converters.IdsConverters;

public class AssetIdConverter : ValueConverter<AssetId, Guid> { public AssetIdConverter() : base(c => c.Value, c => new (c)) { } }