using System.Collections.Generic;

namespace DontGetLost
{
    public static class ListExtensions
    {
        public static List<T> Add<T>(this List<T> target, IEnumerable<T> value)
        {
            target.AddRange(value);
            return target;
        }
    }
}