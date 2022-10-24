using Algorithm.Algos.Algos;
using Algorithm.Factory.Attrbiutes;
using Algorithm.Factory.Interface;
using Common.Attrbiutes;
using GraphLib.Interfaces;

namespace Algorithm.Factory
{
    [Order(4)]
    [WaveGroup]
    public sealed class LeeAlgorithmFactory : IAlgorithmFactory<LeeAlgorithm>
    {
        public LeeAlgorithm Create(IPathfindingRange endPoints)
        {
            return new LeeAlgorithm(endPoints);
        }

        public override string ToString()
        {
            return "Lee algorithm";
        }
    }
}
