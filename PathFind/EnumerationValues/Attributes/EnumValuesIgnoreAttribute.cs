using System;
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
            return enums.All(value => value.GetType().IsEnum)
                ? new HashSet<Enum>(enums.Cast<Enum>())
                : throw new ArgumentException();
        }

        private readonly Lazy<IReadOnlyCollection<Enum>> ignored;
    }
}