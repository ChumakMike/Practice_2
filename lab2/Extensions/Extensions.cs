using System.Collections.Generic;

namespace lab2.Extensions
{
    internal static class Extensions
    {
        public static void AddValue(this List<string> list, string value)
        {
            if (!list.Contains(value))
                list.Add(value);
        }
    }
}
