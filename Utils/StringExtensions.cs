using System.Globalization;

namespace Utils
{
    public static class StringExtensions
    {
        public static bool IsEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        public static double ToDouble(this string str)
        {
            return double.Parse(str, NumberStyles.Any, CultureInfo.InvariantCulture);
        }

        public static long ToLong(this string str)
        {
            return long.Parse(str, NumberStyles.Any, CultureInfo.InvariantCulture);
        }

        public static int ToInt(this string str)
        {
            return int.Parse(str, NumberStyles.Any, CultureInfo.InvariantCulture);
        }
    }
}
