using Common.Attributes;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Common.Extensions
{
    public static class GenericExtensions
    {
        public static bool HasAttribute<TAttribute, T>(this T self)
            where TAttribute : Attribute
        {
            return self
                .GetType()
                .GetAttribute<TAttribute>() != null;
        }

        public static bool IsSerializable<T>(this T self)
        {
            return self.HasAttribute<SerializableAttribute, T>();
        }

        /// <summary>
        /// Creates a deep copy of an object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <returns>A deep copy (new instance) of an object</returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        /// <remarks>The object what <paramref name="self"/> references at 
        /// must be marked with attribute <see cref="SerializableAttribute"/></remarks>
        public static T TryCopyDeep<T>(this T self)
        {
            if (!(self is object))
            {
                throw new ArgumentNullException(nameof(self));
            }

            if (!self.IsSerializable())
            {
                throw new ArgumentException($"{typeof(T).Name} must be serializable");
            }

            var formatter = new BinaryFormatter();
            using (var stream = new MemoryStream())
            {
                formatter.Serialize(stream, self);
                stream.Seek(0, SeekOrigin.Begin);
                return (T)formatter.Deserialize(stream);
            }
        }

        public static bool IsNullObject<T>(this T self)
        {
            return self.HasAttribute<NullAttribute, T>();
        }
    }
}
