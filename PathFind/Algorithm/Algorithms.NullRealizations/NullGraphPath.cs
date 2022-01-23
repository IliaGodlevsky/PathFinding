using Algorithm.Interfaces;
using GraphLib.Interfaces;
using NullObject.Attributes;
using SingletonLib;
using System;

namespace Algorithm.NullRealizations
{
    /// <summary>
    /// A class, that represents a null analog for
    /// <see cref="IGraphPath"/>. This class is a singleton
    /// </summary>
    [Null]
    public sealed class NullGraphPath : Singleton<NullGraphPath>, IGraphPath
    {
        public IVertex[] Path => Array.Empty<IVertex>();

        public double Cost => default;

        public int Length => default;

        private NullGraphPath()
        {

        }
    }
}
