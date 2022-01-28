using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TAttribute GetAttributeOrNull<TAttribute>(this MemberInfo self, bool inherit = false)
            where TAttribute : Attribute
        {
            return Attribute.GetCustomAttribute(self, typeof(TAttribute), inherit) as TAttribute;
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

        public static bool TryGetNonPublicParametrelessCtor(this Type type, out ConstructorInfo ctor)
        {
            var flags = BindingFlags.NonPublic | BindingFlags.Instance;
            ctor = type.GetConstructors(flags).FirstOrDefault(c => c.GetParameters().Length == 0);
            return ctor != null;
        }

        /// <summary>
        /// Determines, where <paramref name="type"/> implements all
        /// of provided <paramref name="interfaces"/>
        /// </summary>
        /// <param name="type"></param>
        /// <param name="interfaces"></param>
        /// <returns>true if type implements all the interfaces and false if is not</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ImplementsAll(this Type type, params Type[] interfaces)
        {
            return interfaces.All(@interface => type.GetInterface(@interface.Name) != null);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static bool TrySetValue(this FieldInfo fieldInfo, object obj, object value)
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
}