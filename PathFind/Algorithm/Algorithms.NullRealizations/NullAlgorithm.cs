using Algorithm.Interfaces;
using System;

namespace Algorithm.NullRealizations
{
    public sealed class NullAlgorithm : IAlgorithm
    {
        public static IAlgorithm Instance => instance.Value;

        public IGraphPath FindPath()
        {
            return new NullGraphPath();
        }

        private NullAlgorithm()
        {

        }

        private static readonly Lazy<IAlgorithm> instance = new Lazy<IAlgorithm>(() => new NullAlgorithm());
    }
}
