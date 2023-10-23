using Shared.Extensions;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Persistence.Ef.Converters;

public class SecureIntegerConverter : ValueConverter<int, string>
{
    public SecureIntegerConverter()
        : base(
            v => v.SecureAmount(null),
            v => v.UnsecureAmount(null))
    {
    }
}