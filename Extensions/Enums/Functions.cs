using System;
using System.Collections.Generic;
using System.Linq;

namespace Extensions.Enums
{
    public static class Functions
    {
        public static IEnumerable<T> GetValues2<T>() 
            => Enum.GetValues(typeof(T)).Cast<T>();

        public static IReadOnlyList<T> GetValues<T>() 
            => (T[])Enum.GetValues(typeof(T));
    }
}
