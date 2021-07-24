using Algorithm.Algos.Attributes;
using Algorithm.Algos.Enums;
using Algorithm.Base;
using Algorithm.Common;
using Algorithm.Interfaces;
using Common.Extensions;
using GraphLib.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Algos.Extensions
{
    public static class AlgorithmsExtensions
    {
        public static IAlgorithm ToInstance(this Algorithms self, IGraph graph, IEndPoints endPoints)
        {
            var algorithmType = self.GetAttributeOrNull<AlgorithmTypeAttribute>()?.AlgorithmType;
            return algorithmType == null || !typeof(IAlgorithm).IsAssignableFrom(algorithmType)
                ? new NullAlgorithm()
                : (IAlgorithm)Activator.CreateInstance(algorithmType, graph, endPoints);
        }
    }
}
