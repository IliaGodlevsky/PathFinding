using Algorithm.Algos.Algos;
using Algorithm.Base;
using Algorithm.Factory.Attrbiutes;
using Common.Attrbiutes;
using GraphLib.Interfaces;
using System.ComponentModel;

namespace Algorithm.Factory
{
    [Order(4)]
    [WaveGroup]
    [Description("Lee algorithm")]
    public sealed class LeeAlgorithmFactory : IAlgorithmFactory
    {
        public PathfindingAlgorithm CreateAlgorithm(IEndPoints endPoints)
        {
            return new LeeAlgorithm(endPoints);
        }
    }
}
