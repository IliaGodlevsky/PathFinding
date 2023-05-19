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
        /// <exception cref="FieldAccessException"></exception>
        /// <exception cref="TargetException"></exception>
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
            return field == null ? default : (TAttribute)field.GetValue(null);
        }
    }
}