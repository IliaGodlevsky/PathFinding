using System;

namespace NullObject.Attributes
{
    /// <summary>
    /// Indicates, that the class is used as an optional 
    /// (default) realization of hierarchy of classes
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public sealed class NullAttribute : Attribute
    {

    }
}
