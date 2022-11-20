using System;
using System.Reflection;

namespace Shared.Extensions
{
    public static class MemberInfoExtensions
    {
        public static TAttribute GetAttributeOrDefault<TAttribute>(this MemberInfo self, 
            bool inherit = false, string defaultFieldName = "Default")
            where TAttribute : Attribute
        {
            var attributeType = typeof(TAttribute);
            if (Attribute.IsDefined(self, attributeType))
            {
                return (TAttribute)Attribute.GetCustomAttribute(self, attributeType, inherit);
            }
            var flags = BindingFlags.Static | BindingFlags.Public 
                | BindingFlags.FlattenHierarchy | BindingFlags.Instance;
            var property = attributeType.GetField(defaultFieldName, flags);
            return property == null 
                ? default(TAttribute) 
                : (TAttribute)property.GetValue(null);
        }

        public static bool TryCreateDelegate<TDelegate>(this MethodInfo self, object target,
            out TDelegate action) where TDelegate : Delegate
        {
            try
            {
                if (self == null)
                {
                    throw new ArgumentNullException(nameof(self));
                }
                action = (TDelegate)self.CreateDelegate(typeof(TDelegate), target);
                return true;
            }
            catch
            {
                action = null;
                return false;
            }
        }
    }
}