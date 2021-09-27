using System;
using WPFVersion3D.Interface;

namespace WPFVersion3D.Attributes
{
    [AttributeUsage(AttributeTargets.All, Inherited = true)]
    internal abstract class BaseAnimationSpeed : Attribute, IAnimationSpeed
    {
        public abstract double Milliseconds { get; }
    }
}
