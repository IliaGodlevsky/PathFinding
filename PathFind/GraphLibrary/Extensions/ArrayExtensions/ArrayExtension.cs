using System;

namespace WpfVersion.Extensions.ArrayExtension
{
    public static class ArrayExtension
    {
        public static bool Exists(this Array arr, Predicate<object> predicate)
        {
            foreach (var obj in arr)
                if (predicate(obj))
                    return true;
            return false;
        }
    }
}
