using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace Infrastructure.Persistence.Ef;

public class StronglyTypedIdValueGenerator<T> : ValueGenerator<T>
{
    public override bool GeneratesTemporaryValues => false;

    public override T Next(EntityEntry entry)
    {
        return default!;
    }
}