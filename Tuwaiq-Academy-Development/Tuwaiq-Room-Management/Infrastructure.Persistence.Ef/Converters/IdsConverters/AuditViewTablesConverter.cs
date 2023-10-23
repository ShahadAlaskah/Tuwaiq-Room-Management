using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;

namespace Infrastructure.Persistence.Ef.Converters.IdsConverters;

public class AuditViewTablesConverter : ValueConverter<string[]?, string>
{
    public AuditViewTablesConverter()
        : base(
            value => JsonConvert.SerializeObject(value),
            serializedValue => JsonConvert.DeserializeObject<string[]?>(serializedValue))
    {
    }
}