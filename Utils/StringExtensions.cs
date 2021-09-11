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

        public static decimal ToDecimal(this string str)
        {
            return decimal.Parse(str, NumberStyles.Any, CultureInfo.InvariantCulture);
        }

        public static decimal? ToNullableDecimal(this string str)
        {
            if (decimal.TryParse(str, NumberStyles.Any, CultureInfo.InvariantCulture, out var result))
            {
                return result;
            }

            return null;
        }

        public static int? ToNullableInt(this string str)
        {
            if (int.TryParse(str, NumberStyles.Any, CultureInfo.InvariantCulture, out var result))
            {
                return result;
            }

            return null;
        }
    }
}
