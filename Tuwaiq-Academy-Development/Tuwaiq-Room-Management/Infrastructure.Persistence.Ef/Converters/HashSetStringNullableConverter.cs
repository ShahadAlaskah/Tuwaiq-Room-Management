using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;

namespace Infrastructure.Persistence.Ef.Converters;

public class HashSetStringNullableConverter : ValueConverter<HashSet<string>?, string>
{
    public HashSetStringNullableConverter()
        : base(
            value => JsonConvert.SerializeObject(value),
            serializedValue => JsonConvert.DeserializeObject<HashSet<string>?>(serializedValue))
    {
    }
}