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
            return double.Parse(str, System.Globalization.NumberStyles.Any, CultureInfo.InvariantCulture);
        }
    }
}
