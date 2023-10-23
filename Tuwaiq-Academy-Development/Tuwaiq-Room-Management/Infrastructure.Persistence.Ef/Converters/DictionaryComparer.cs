using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Infrastructure.Persistence.Ef.Converters;

public class DictionaryComparer : ValueComparer<Dictionary<string, object>?>
{
    public DictionaryComparer() : base(
        (c1, c2) => c1!.SequenceEqual(c2!),
        t => t!.GetHashCode())
    {
    }
}