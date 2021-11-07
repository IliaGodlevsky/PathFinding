using Algorithm.Interfaces;
using GraphLib.Interfaces;
using GraphLib.NullRealizations.NullObjects;
using NullObject.Attributes;

namespace Algorithm.NullRealizations
{
    [Null]
    public sealed class NullGraphPath : IGraphPath
    {
        public NullGraphPath()
        {
            Path = new NullVertex[] { };
        }

        public IVertex[] Path { get; }

        public double Cost => default;

        public int Length => default;
    }
}
