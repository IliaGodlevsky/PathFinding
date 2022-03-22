using Algorithm.Algos.Algos;
using Algorithm.Factory.Attrbiutes;
using Algorithm.Interfaces;
using Common.Attrbiutes;
using GraphLib.Interfaces;
using System.ComponentModel;

namespace Algorithm.Factory
{
    [Order(4)]
    [WaveGroup]
    [Description("Lee algorithm")]
    public sealed class LeeAlgorithmFactory : IAlgorithmFactory<LeeAlgorithm>
    {
        public LeeAlgorithm Create(IEndPoints endPoints)
        {
            return new LeeAlgorithm(endPoints);
        }
    }
}
