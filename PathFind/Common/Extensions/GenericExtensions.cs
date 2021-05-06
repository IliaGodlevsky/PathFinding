using Common.Attributes;
using System;

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

        public static bool IsNullObject<T>(this T self)
        {
            return self.HasAttribute<NullAttribute, T>();
        }
    }
}
