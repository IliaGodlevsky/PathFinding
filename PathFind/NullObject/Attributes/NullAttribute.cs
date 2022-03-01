using System;

namespace NullObject.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, Inherited = true)]
    public sealed class NullAttribute : Attribute
    {

    }
}
