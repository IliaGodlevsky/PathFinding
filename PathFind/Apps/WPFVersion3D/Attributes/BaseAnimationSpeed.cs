using System;
using WPFVersion3D.Interface;

namespace WPFVersion3D.Attributes
{
    internal abstract class BaseAnimationSpeed : Attribute, IAnimationSpeed
    {
        public abstract double Milliseconds { get; }
    }
}
