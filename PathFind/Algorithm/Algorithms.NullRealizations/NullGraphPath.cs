using Algorithm.Interfaces;
using GraphLib.Interfaces;
using GraphLib.NullRealizations.NullObjects;
using NullObject.Attributes;
using System;

namespace Algorithm.NullRealizations
{
    /// <summary>
    /// A class, that represents a null analog for
    /// <see cref="IGraphPath"/>. This class is a singleton
    /// </summary>
    [Null]
    public sealed class NullGraphPath : IGraphPath
    {
        /// <summary>
        /// Returns an intance of <see cref="NullGraphPath"/>
        /// </summary>
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
