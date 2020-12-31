using System;
using System.Reflection;

namespace Common.Extensions
{
    public static class TypeExtensions
    {
        public static Assembly GetAssembly(this Type self)
        {
            return Assembly.Load(self.Assembly.GetName());
        }

        public static ConstructorInfo GetConstructor(this Type self, params Type[] parametres)
        {
            return self.GetConstructor(parametres);
        }

        public static TAttribute GetAttribute<TAttribute>(this MemberInfo self, bool inherit = false) 
            where TAttribute : Attribute
        {
            return (TAttribute)Attribute.GetCustomAttribute(self, typeof(TAttribute), inherit);
        }

        public static bool TryCreateDelegate<TDelegate>(this MethodInfo self, 
            object target, out TDelegate del)
            where TDelegate : Delegate
        {
            try
            {
                del = (TDelegate)self.CreateDelegate(typeof(TDelegate), target);
                return true;
            }
            catch
            {
                del = null;
                return false;
            }
        }
    }
}
