using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;

namespace Infrastructure.Persistence.Ef.Converters.IdsConverters;

public class AuditViewArgsConverter : ValueConverter<Dictionary<string, object>?, string>
{
    public AuditViewArgsConverter()
        : base(
            value => JsonConvert.SerializeObject(value),
            serializedValue => JsonConvert.DeserializeObject<Dictionary<string, object>?>(serializedValue))
    {
    }
}