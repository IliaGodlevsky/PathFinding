using Shared.Extensions;
using Shared.Primitives.Attributes;

namespace Shared.Primitives.Extensions
{
    public static class ObjectExtensions
    {
        public static int GetOrder(this object self)
        {
            int order = self.GetAttributeOrDefault<OrderAttribute>().Order;
            return order;
        }
    }
}
