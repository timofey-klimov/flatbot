namespace Utils
{
    public static class TypeExtensions
    {
        public static string GetTypeName(this object obj)
        {
            return obj.GetType().Name;
        }
    }
}
