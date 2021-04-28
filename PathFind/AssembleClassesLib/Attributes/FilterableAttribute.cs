using System;

namespace AssembleClassesLib.Attributes
{
    /// <summary>
    /// Indicates that a class should be ignored when fetching classes from an assembly
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public sealed class FilterableAttribute : Attribute
    {

    }
}
