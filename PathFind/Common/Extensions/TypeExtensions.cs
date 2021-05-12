using System;
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
        /// Creates delegate of type <typeparamref name="TDelegate"/> from the method 
        /// </summary>
        /// <typeparam name="TDelegate"></typeparam>
        /// <param name="self"></param>
        /// <param name="target"></param>
        /// <param name="del"></param>
        /// <returns><see cref="true"/> if the creation of the 
        /// <typeparamref name="TDelegate"/> was successful and false if wasn't</returns>
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
