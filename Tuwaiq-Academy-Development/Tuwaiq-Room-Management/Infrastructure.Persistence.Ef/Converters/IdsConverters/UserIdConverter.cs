using Shared.Ids;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Persistence.Ef.Converters.IdsConverters;

public class UserIdConverter : ValueConverter<UserId, string> { public UserIdConverter() : base(c => c.Value, c => new (c)) { } }