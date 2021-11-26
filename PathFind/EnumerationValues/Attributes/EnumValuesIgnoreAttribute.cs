using System;

namespace EnumerationValues.Attributes
{
    /// <summary>
    /// Marks enum fields that should be ignored
    /// by specialized classes
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class EnumValuesIgnoreAttribute : Attribute
    {
    }
}
