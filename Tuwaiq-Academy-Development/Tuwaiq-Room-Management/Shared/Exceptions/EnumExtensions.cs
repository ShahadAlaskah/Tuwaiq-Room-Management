using System.ComponentModel;

namespace Shared.Exceptions
{
    public static class EnumExtensions
    {

        public static string GetDescription(this Enum enumValue)
        {
            var fi = enumValue.GetType().GetField(enumValue.ToString());

            var attributes = (DescriptionAttribute[])fi!.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes.Length > 0)
                return attributes[0].Description;
            return enumValue.ToString();
        }

        public static List<string> GetDescriptions<T>() where T : struct
        {
            var result = new List<string>();

            foreach (var ss in Enum.GetNames(typeof(T)))
            {
                var s = typeof(T).GetField(ss.ToString());//.GetDescription()

                var attributes = (DescriptionAttribute[])s!.GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attributes.Length > 0)
                    result.Add(attributes[0].Description);
                else
                    result.Add(s.ToString()!);
            }


            return result;
        }

        public static Dictionary<string, string> GetDescriptionsWithEnum<T>() where T : struct
        {
            var result = new Dictionary<string, string>();

            foreach (var ss in Enum.GetNames(typeof(T)))
            {
                var s = typeof(T).GetField(ss.ToString());//.GetDescription()

                var attributes = (DescriptionAttribute[])s!.GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attributes.Length > 0)
                    result.Add(ss, attributes[0].Description);
                else
                    result.Add(ss, s.ToString()!);
            }


            return result;
        }

        public static Dictionary<int, string> GetAllEnums<T>() where T : struct
        {
            var result = new Dictionary<int, string>();

            foreach (var ss in Enum.GetNames(typeof(T)))
            {
                var s = typeof(T).GetField(ss.ToString());//.GetDescription()

                var attributes = (DescriptionAttribute[])s!.GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attributes.Length > 0)
                    result.Add((int)Enum.Parse(typeof(T), ss.ToString()), attributes[0].Description);
                else
                    result.Add((int)Enum.Parse(typeof(T), ss.ToString()), s.ToString()!);
            }


            return result;
        }

        public static T? GetEnum<T>(string value) where T : struct
        {
            foreach (var ss in Enum.GetNames(typeof(T)))
            {
                var s = typeof(T).GetField(ss.ToString());//.GetDescription()

                var attributes = (DescriptionAttribute[])s!.GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attributes.Length > 0)
                    if (attributes[0].Description == value)
                        return (T)Enum.Parse(typeof(T), ss, true);
            }

            return null;
        }
    }
}