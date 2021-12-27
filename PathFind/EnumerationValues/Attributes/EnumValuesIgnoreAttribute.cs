using Common.Extensions.EnumerableExtensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace EnumerationValues.Attributes
{
    /// <summary>
    /// Marks enums to ignore by specified classes
    /// </summary>
    [AttributeUsage(AttributeTargets.Enum, AllowMultiple = true)]
    public sealed class EnumValuesIgnoreAttribute : Attribute
    {
        public IReadOnlyCollection<Enum> Ignored => ignored.Value;

        public EnumValuesIgnoreAttribute(params object[] values)
        {
            ignored = new Lazy<IReadOnlyCollection<Enum>>(() => GetEnums(values));
        }

        private IReadOnlyCollection<Enum> GetEnums(IEnumerable<object> enums)
        {
            return enums.All(IsEnum) && AreAllOfSameType(enums)
                ? new HashSet<Enum>(enums.Cast<Enum>())
                : throw new ArgumentException();
        }

        private static bool IsEnum(object value)
        {
            return value.GetType().IsEnum;
        }

        private static bool AreAllOfSameType(IEnumerable<object> enums)
        {
            return enums.Select(@enum => @enum.GetType()).AreAllEqual();
        }

        private readonly Lazy<IReadOnlyCollection<Enum>> ignored;
    }
}