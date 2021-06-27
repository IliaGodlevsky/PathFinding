using System;

namespace AssembleClassesLib.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.Struct, Inherited = false)]
    public sealed class LoadableAttribute : Attribute
    {
    }
}
