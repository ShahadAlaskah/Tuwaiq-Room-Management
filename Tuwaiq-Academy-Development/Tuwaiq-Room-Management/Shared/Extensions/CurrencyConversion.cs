using HashidsNet;

namespace Shared.Extensions;

public static class CurrencyConversion
{
    public static int ConvertToAmount(this decimal amount)
    {
        return Convert.ToInt32(Math.Truncate(100 * amount) / 100 * 100);
    }

    public static decimal ConvertFormAmount(this int amount)
    {
        return Convert.ToDecimal(amount / 100);
    }

    public static int UnsecureAmount(this string? hash, string? hashId = null)
    {
        var hashids = new Hashids(hashId ?? "API", 8);
        if (string.IsNullOrEmpty(hash)) return 0;

        var amount = hashids.Decode(hash);
        if (amount == null) return 0;
        return amount.LastOrDefault();
    }

    public static string SecureAmount(this int[] hash, string? hashId = null)
    {
        var hashids = new Hashids(hashId ?? "API", 8);
        if (hash == null || !hash.Any()) throw new Exception("InvalidKeys");

        var amount = hashids.Encode(hash);

        return amount;
    }

    public static string SecureAmount(this int hash, string? hashId = null)
    {
        var hashids = new Hashids(hashId ?? "API", 8);
        if (hash < 0) throw new Exception("InvalidValues");

        var amount = hashids.Encode(new[] { hash });

        return amount;
    }

    public static string Secure(this int[] hash, string? hashId = null)
    {
        var hashids = new Hashids(hashId ?? "API", 8);
        if (hash.Length == 0) throw new Exception("InvalidValues");

        var amount = hashids.Encode(hash);

        return amount;
    }

    public static int[] Unsecure(this string? hash, string? hashId = null)
    {
        var hashids = new Hashids(hashId ?? "API", 8);
        if (string.IsNullOrEmpty(hash)) return new[] { 0 };

        var amount = hashids.Decode(hash);
        if (amount == null) return new[] { 0 };
        return amount;
    }
    
    public static IEnumerable<decimal> Divide(this decimal total, int divider)
    {
        if (divider == 0)
        {
            yield return 0;
        }
        else
        {
            while(divider > 0)
            {
                // determine the next amount to return...
                var partialAmount = Math.Round(total / divider, 0);
                yield return partialAmount;
                // reduce th remaining amount and #buckets
                // to account for previously yielded values
                total -= partialAmount;
                divider--;
            }
        }
    }
}