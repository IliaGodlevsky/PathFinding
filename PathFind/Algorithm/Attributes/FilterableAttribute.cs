using System;

namespace Algorithm.Attributes
{
    /// <summary>
    /// Indicates that the class should be ignored when fetching classes from an assembly
    /// </summary>
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = false)]
    internal sealed class FilterableAttribute : Attribute
    {

    }
}
