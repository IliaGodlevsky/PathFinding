using System;

namespace Common.Attributes
{
    /// <summary>
    /// Indicates, that the class is used as an optional 
    /// (default) realization of hierarchy of classes
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public sealed class DefaultAttribute : Attribute
    {

    }
}
