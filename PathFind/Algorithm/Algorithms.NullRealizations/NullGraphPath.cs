using Algorithm.Interfaces;
using GraphLib.Interfaces;
using GraphLib.NullRealizations.NullObjects;
using NullObject.Attributes;
using System;

namespace Algorithm.NullRealizations
{
    [Null]
    public sealed class NullGraphPath : IGraphPath
    {
        public static IGraphPath Instance => instance.Value;
        private NullGraphPath()
        {
            Path = new NullVertex[] { };
        }

        public IVertex[] Path { get; }

        public double Cost => default;

        public int Length => default;

        private static Lazy<IGraphPath> instance 
            = new Lazy<IGraphPath>(() => new NullGraphPath());
    }
}
