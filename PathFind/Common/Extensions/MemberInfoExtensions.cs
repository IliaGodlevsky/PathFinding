using Common.Attrbiutes;
using System;
using System.Linq;
using System.Reflection;

namespace Common.Extensions
{
    public static class MemberInfoExtensions
    {
        public static TAttribute GetAttributeOrNull<TAttribute>(this MemberInfo self, bool inherit = false)
            where TAttribute : Attribute
        {
            return Attribute.GetCustomAttribute(self, typeof(TAttribute), inherit) as TAttribute;
        }

        public static int GetOrder(this MethodInfo self)
        {
            return self.GetAttributeOrNull<OrderAttribute>()?.Order
                ?? OrderAttribute.Default.Order;
        }

        public static bool TryCreateDelegate<TDelegate>(this MethodInfo self, object target,
            out TDelegate action) where TDelegate : Delegate
        {
            try
            {
                action = (TDelegate)self.CreateDelegate(typeof(TDelegate), target);
                return true;
            }
            catch
            {
                action = null;
                return false;
            }
        }

        public static bool TryGetNonPublicParametrelessCtor(this Type type, out ConstructorInfo ctor)
        {
            var flags = BindingFlags.NonPublic | BindingFlags.Instance;
            ctor = type.GetConstructors(flags).FirstOrDefault(c => c.GetParameters().Length == 0);
            return ctor != null;
        }

        public static bool ImplementsAll(this Type type, params Type[] interfaces)
        {
            return interfaces.All(@interface => type.GetInterface(@interface.Name) != null);
        }

        public static bool TrySetValue(this FieldInfo fieldInfo, object obj, object value)
        {
            var locker = new object();

            lock (locker)
            {
                if (fieldInfo == null)
                {
                    return false;
                }
                try
                {
                    fieldInfo.SetValue(obj, value);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        public static bool IsAttachedTo<T>(this Type type, T attachedTo)
        {
            var attribute = type.GetAttributeOrNull<AttachedToAttribute>();
            return attribute?.IsAttachedTo(attachedTo.GetType()) == true;
        }

        public static bool Implements<TInterface>(this Type type)
            where TInterface : class
        {
            return type.ImplementsAll(typeof(TInterface));
        }
    }
}