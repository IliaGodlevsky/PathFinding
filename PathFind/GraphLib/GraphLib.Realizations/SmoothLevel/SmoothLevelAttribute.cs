using GraphLib.Interfaces;
using System;

namespace GraphLib.Realizations.SmoothLevel
{
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class SmoothLevelAttribute : Attribute, ISmoothLevel
    {
        public int Level { get; }

        public SmoothLevelAttribute(int level)
        {
            Level = level;
        }
    }
}