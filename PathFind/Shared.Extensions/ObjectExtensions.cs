using System;
using System.Reflection;

namespace Shared.Extensions
{
    public static class ObjectExtensions
    {
        public static TAttribute GetAttributeOrNull<TAttribute>(this object self)
            where TAttribute : Attribute
        {
            var memberInfo = self is Enum e
                ? e.GetType().GetField(e.ToString())
                : (MemberInfo)self.GetType();
            return memberInfo.GetAttributeOrNull<TAttribute>();
        }
    }
}
