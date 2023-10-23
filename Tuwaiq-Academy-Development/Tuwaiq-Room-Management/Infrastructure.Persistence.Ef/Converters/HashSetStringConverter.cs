using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;

namespace Infrastructure.Persistence.Ef.Converters;

public class HashSetStringConverter : ValueConverter<HashSet<string>, string>
{
    public HashSetStringConverter()
        : base(
            value => JsonConvert.SerializeObject(value),
            serializedValue => JsonConvert.DeserializeObject<HashSet<string>>(serializedValue)!)
    {
    }
}