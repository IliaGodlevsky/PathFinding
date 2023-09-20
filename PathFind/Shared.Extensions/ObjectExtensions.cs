using System;
using System.Reflection;

namespace Shared.Extensions
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
    }
}
