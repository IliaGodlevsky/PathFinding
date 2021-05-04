using System;

namespace AssembleClassesLib.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public sealed class LoadableAttribute : Attribute
    {
    }
}
