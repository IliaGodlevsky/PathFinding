using System.Collections;
using System.Linq;

namespace NullObject.Extensions
{
    public static class EnumerableExtensions
    {
        public static bool ContainsNulls(this IEnumerable self)
        {
            return self.OfType<object>().Any(ObjectExtensions.IsNull);
        }
    }
}
