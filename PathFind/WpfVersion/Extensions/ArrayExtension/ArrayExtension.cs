using System;

namespace WpfVersion.Extensions.ArrayExtension
{
    public static class ArrayExtension
    {
        public static bool Exists(this Array arr, Predicate<object> pred)
        {
            foreach (var obj in arr)
                if (pred(obj))
                    return true;
            return false;
        }
    }
}
