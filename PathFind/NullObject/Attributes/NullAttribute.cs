using System;

namespace NullObject.Attributes
{
    /// <summary>
    /// Indicates, that a class is used as an optional 
    /// (default) realization of hierarchy of classes
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, Inherited = false)]
    public sealed class NullAttribute : Attribute
    {

    }
}
