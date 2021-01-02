using System;

namespace Algorithm.Attributes
{
    /// <summary>
    /// Indicates that a class should be ignored when fetching classes from an assembly
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    internal sealed class FilterableAttribute : Attribute
    {

    }
}
