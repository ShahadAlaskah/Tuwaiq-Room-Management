using Shared.Extensions;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Persistence.Ef.Converters;

public class SecureMoneyConverter : ValueConverter<decimal, string>
{
    public SecureMoneyConverter()
        : base(
            v => v.ConvertToAmount().SecureAmount(null),
            v => v.UnsecureAmount(null).ConvertFormAmount())
    {
    }
}