using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;

namespace Infrastructure.Persistence.Ef.Converters;

public class DictionaryDecimalStringConverter : ValueConverter<Dictionary<decimal, string>, string>
{
    public DictionaryDecimalStringConverter()
        : base(
            value => JsonConvert.SerializeObject(value),
            serializedValue => JsonConvert.DeserializeObject<Dictionary<decimal, string>>(serializedValue))
    {
    }
}