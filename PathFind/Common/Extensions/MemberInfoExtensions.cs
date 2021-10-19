using System;
using System.Linq;
using System.Reflection;

namespace Common.Extensions
{
    public static class MemberInfoExtensions
    {
        /// <summary>
        /// Returns attribute that specified as <typeparamref name="TAttribute"/>
        /// </summary>
        /// <typeparam name="TAttribute"></typeparam>
        /// <param name="self"></param>
        /// <param name="inherit"></param>
        /// <returns>Attribute of type <typeparamref name="TAttribute"/> 
        /// if it exists and <see cref="null"/> if it doesn't</returns>
        public static TAttribute GetAttributeOrNull<TAttribute>(this MemberInfo self, bool inherit = false)
            where TAttribute : Attribute
        {
            return (TAttribute)Attribute.GetCustomAttribute(self, typeof(TAttribute), inherit);
        }

        /// <summary>
        /// Creates a delegate of type <typeparamref name="TDelegate"/> from the method 
        /// </summary>
        /// <typeparam name="TDelegate"></typeparam>
        /// <param name="self"></param>
        /// <param name="target"></param>
        /// <param name="action"></param>
        /// <returns><see cref="true"/> if the creation of the 
        /// <typeparamref name="TDelegate"/> was successful and false if wasn't</returns>
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

        public static bool Implements<TInterface>(this Type type) where TInterface : class
        {
            return type.GetInterface(typeof(TInterface).Name) != null;
        }

        public static TValue[] GetValuesOfStaticClassProperties<TValue>(this Type classType, params string[] exceptNamesOfProperties)
        {
            return classType
                .GetProperties()
                .Where(property => !exceptNamesOfProperties.Contains(property.Name))
                .Select(property => property.GetValue(null))
                .OfType<TValue>()
                .ToArray();
        }
    }
}