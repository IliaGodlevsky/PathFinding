using Algorithm.Algos.Attributes;
using Algorithm.Common;
using Algorithm.Interfaces;
using Algorithm.Realizations.Enums;
using Common.Extensions;
using GraphLib.Interfaces;
using System;

namespace Algorithm.Realizations
{
    public static class AlgorithmFactory
    {
        public static IAlgorithm CreateAlgorithm(Algorithms algorithm, IGraph graph, IEndPoints endPoints)
        {
            var algorithmType = algorithm.GetAttributeOrNull<AlgorithmTypeAttribute>()?.AlgorithmType;
            return algorithmType == null || !typeof(IAlgorithm).IsAssignableFrom(algorithmType)
                ? new NullAlgorithm() 
                : (IAlgorithm)Activator.CreateInstance(algorithmType, graph, endPoints);
        }
    }
}