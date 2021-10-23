using GraphLib.Interfaces;
using System;

namespace GraphLib.Realizations.SmoothLevel
{
    [AttributeUsage(AttributeTargets.Field, Inherited = true)]
    public sealed class SmoothLevelAttribute : Attribute, ISmoothLevel
    {
        public SmoothLevelAttribute(int level)
        {
            Level = level;
        }

        public int Level { get; }
    }
}