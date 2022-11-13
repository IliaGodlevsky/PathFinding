using Shared.Extensions;
using Shared.Primitives.Attributes;

namespace Shared.Primitives.Extensions
{
    public static class ObjectExtensions
    {
        public static int GetOrderOrDefault(this object self)
        {
            return self.GetAttributeOrNull<OrderAttribute>()?.Order ?? OrderAttribute.Default.Order;
        }
    }
}
