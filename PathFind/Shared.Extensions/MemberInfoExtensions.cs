using System;
using System.Reflection;

namespace Shared.Extensions
{
    public static class MemberInfoExtensions
    {
        /// <summary>
        /// Attemps to fetch an attribute <typeparamref name="TAttribute"/>
        /// applied to <paramref name="self"/>
        /// </summary>
        /// <typeparam name="TAttribute"></typeparam>
        /// <param name="self"></param>
        /// <param name="inherit"></param>
        /// <param name="defaultFieldName">name of the field, that is considered to be default</param>
        /// <returns>An attribute, that was applied to a member or default value 
        /// of the attribute if it was defined, otherwise - null</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="NotSupportedException"></exception>
        /// <exception cref="AmbiguousMatchException"></exception>
        /// <exception cref="TypeLoadException"></exception>
        /// <remarks>A public static field is considered as a default value of an attribute</remarks>
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
            var field = attributeType.GetField(defaultFieldName, flags);
            return field == null 
                ? default(TAttribute) 
                : (TAttribute)field.GetValue(null);
        }

        /// <summary>
        /// Attemps to create a delegate from information about method
        /// </summary>
        /// <typeparam name="TDelegate"></typeparam>
        /// <param name="self">an information about method from which delegate should be created</param>
        /// <param name="target">an object, from which method the information was taken</param>
        /// <param name="action">an output parametre, which contains a result of 
        /// operation or null if delegate can't be created</param>
        /// <returns>true if delegate was created, otherwise - false </returns>
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