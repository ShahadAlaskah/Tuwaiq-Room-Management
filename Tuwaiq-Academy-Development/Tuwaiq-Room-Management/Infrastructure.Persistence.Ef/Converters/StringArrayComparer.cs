using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Infrastructure.Persistence.Ef.Converters;

public class StringArrayComparer : ValueComparer<string[]?>
{
    public StringArrayComparer() : base(
        (c1, c2) => c1!.SequenceEqual(c2!),
        t => t!.GetHashCode())
    {
    }
}