using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;

namespace Infrastructure.Persistence.Ef.Converters;

public class DictionaryConverter : ValueConverter<Dictionary<string, string>, string>
{
    public DictionaryConverter()
        : base(
            value => JsonConvert.SerializeObject(value),
            serializedValue => JsonConvert.DeserializeObject<Dictionary<string, string>>(serializedValue))
    {
    }
}