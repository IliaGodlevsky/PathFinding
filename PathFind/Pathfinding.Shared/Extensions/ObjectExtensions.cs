using System;
using System.Reflection;

namespace Pathfinding.Shared.Extensions
{
    public static class ObjectExtensions
    {
        public static TAttribute GetAttributeOrDefault<TAttribute>(this object self)
            where TAttribute : Attribute
        {
            MemberInfo info = self switch
            {
                Enum e => e.GetType().GetField(e.ToString()),
                Type t => t,
                MemberInfo i => i,
                _ => self.GetType()
            };
            return info.GetAttributeOrDefault<TAttribute>();
        }

        public static int GetOrder(this object self)
        {
            int order = self.GetAttributeOrDefault<OrderAttribute>().Order;
            return order;
        }

        public static object GetGroupToken(this object self)
        {
            object groupToken = self.GetAttributeOrDefault<GroupAttribute>().GroupToken;
            return groupToken;
        }
    }
}
