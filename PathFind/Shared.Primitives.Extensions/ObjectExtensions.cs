using Shared.Extensions;
using Shared.Primitives.Attributes;

namespace Shared.Primitives.Extensions
{
    public static class ObjectExtensions
    {
        public static int GetOrder(this object self)
        {
            return self.GetAttributeOrDefault<OrderAttribute>().Order;
        }
    }
}
