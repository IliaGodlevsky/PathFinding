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

        /// <summary>
        /// Returns public constructor paramtres 
        /// that are identical to <paramref name="parametres"/>
        /// </summary>
        /// <param name="self"></param>
        /// <param name="parametres"></param>
        /// <returns>A public constructor if such exists with specified 
        /// <paramref name="parametres"/> of <see cref="null"/> if doesn't</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static ConstructorInfo GetConstructor(this Type self, params Type[] parametres)
        {
            return self.GetConstructor(parametres);
        }

        /// <summary>
        /// Returns attribute that specified as <typeparamref name="TAttribute"/>
        /// </summary>
        /// <typeparam name="TAttribute"></typeparam>
        /// <param name="self"></param>
        /// <param name="inherit"></param>
        /// <returns>Attribute of type <typeparamref name="TAttribute"/> 
        /// if it exists and <see cref="null"/> if it doesn't</returns>
        public static TAttribute GetAttribute<TAttribute>(this MemberInfo self, bool inherit = false)
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
