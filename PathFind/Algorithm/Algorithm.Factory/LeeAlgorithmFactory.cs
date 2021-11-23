using Algorithm.Algos.Algos;
using Algorithm.Base;
using Algorithm.Factory.Attrbiutes;
using GraphLib.Interfaces;
using System.ComponentModel;

namespace Algorithm.Factory
{
    [WaveGroup(4)]
    [Description("Lee algorithm")]
    public sealed class LeeAlgorithmFactory : IAlgorithmFactory
    {
        public PathfindingAlgorithm CreateAlgorithm( IEndPoints endPoints)
        {
            return new LeeAlgorithm(endPoints);
        }
    }
}
