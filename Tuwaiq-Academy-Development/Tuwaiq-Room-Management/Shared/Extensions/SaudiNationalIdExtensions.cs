using System.Text.RegularExpressions;

namespace Shared.Extensions;

public static class SaudiNationalIdExtensions
{
    public static NationIdType NationalIdCheck(string id)
    {
        id = id.Trim();
        if (!Regex.IsMatch(id, @"[0-9]+"))
        {
            return NationIdType.Invalid;
        }

        if (id.Length != 10)
        {
            return NationIdType.Invalid;
        }

        int type = (int)(id[0] - '0');
        if (type != 2 && type != 1)
        {
            return NationIdType.Invalid;
        }

        int sum = 0;
        for (int i = 0; i < 10; i++)
        {
            if (i % 2 == 0)
            {
                string zfOdd = ((int)(id[i] - '0') * 2).ToString().PadLeft(2, '0');
                sum += (int)(zfOdd[0] - '0') + (int)(zfOdd[1] - '0');
            }
            else
            {
                sum += (int)(id[i] - '0');
            }
        }

        return (sum % 10 != 0) ? NationIdType.Invalid : (NationIdType)type;
    }
    
    public static bool IsNationalId(this string id)
    {
        return NationalIdCheck(id) != NationIdType.Invalid;
    }
}