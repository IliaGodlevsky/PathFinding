using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
