using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;

namespace Infrastructure.Persistence.Ef.Converters;

public class DictionaryNullableConverter : ValueConverter<Dictionary<string, string>?, string>
{
    public DictionaryNullableConverter()
        : base(
            value => JsonConvert.SerializeObject(value),
            serializedValue => JsonConvert.DeserializeObject<Dictionary<string, string>?>(serializedValue))
    {
    }
}