using System;
using WPFVersion3D.Interface;

namespace WPFVersion3D.Attributes
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple =false, Inherited = true)]
    internal abstract class BaseSpeed : Attribute, IAnimationSpeed
    {
        public abstract double Milliseconds { get; }
    }
}
