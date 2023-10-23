using System.Globalization;

namespace Shared.Helpers;

public class EnumValueDto
{
    public int Key { get; set; }

    public string Value { get; set; } = null!;

    public static ICollection<EnumValueDto> ConvertEnumToList<T>() where T : struct, IConvertible
    {
        if (!typeof(T).IsEnum)
        {
            throw new Exception("Type given T must be an Enum");
        }

        var result = Enum.GetValues(typeof(T))
                         .Cast<T>()
                         .Select(x =>  new EnumValueDto { Key = Convert.ToInt32(x), 
                                       Value = x.ToString(new CultureInfo("en")) })
                         .ToList()
                         .AsReadOnly();

        return result;
    }
}