using Common.Attributes;
using System;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;

namespace Common.Extensions
{
    public static class TypeExtensions
    {
        public static bool IsFilterable(this Type self)
        {
            return self.GetAttribute<FilterableAttribute>() != null;
        }

        public static bool IsSerializable<T>(this T self)
        {
            return self.GetType().GetAttribute<SerializableAttribute>() != null;
        }

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

        /// <summary>
        /// Creates a deep copy of an object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <returns>A deep copy (new instance) of an object</returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        /// <remarks>The object <paramref name="self"/> points at 
        /// must be marked with attribute <see cref="SerializableAttribute"/></remarks>
        public static T DeepCopy<T>(this T self)
        {
            if (!(self is object))
            {
                throw new ArgumentNullException(nameof(self));
            }

            if (!self.IsSerializable())
                throw new ArgumentException("Type must be serializable");

            var formatter = new BinaryFormatter();
            using (var stream = new MemoryStream())
            {
                formatter.Serialize(stream, self);
                stream.Seek(0, SeekOrigin.Begin);
                return (T)formatter.Deserialize(stream);
            }
        }
    }
}
