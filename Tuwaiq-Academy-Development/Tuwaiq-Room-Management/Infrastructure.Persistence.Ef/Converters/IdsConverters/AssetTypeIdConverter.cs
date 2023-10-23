using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Shared.Ids;

namespace Infrastructure.Persistence.Ef.Converters.IdsConverters;

public class AssetTypeIdConverter : ValueConverter<AssetTypeId, Guid> { public AssetTypeIdConverter() : base(c => c.Value, c => new (c)) { } }