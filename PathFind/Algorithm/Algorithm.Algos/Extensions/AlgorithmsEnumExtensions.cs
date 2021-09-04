using Algorithm.Algos.Attributes;
using Algorithm.Algos.Enums;
using Algorithm.Interfaces;
using Algorithm.NullRealizations;
using Common.Extensions;
using GraphLib.Interfaces;
using System;

namespace Algorithm.Algos.Extensions
{
    public static class AlgorithmsEnumExtensions
    {
        public static IAlgorithm ToAlgorithm(this Algorithms self, 
            IGraph graph, IIntermediateEndPoints endPoints)
        {
            var algorithmType = self.GetAttributeOrNull<AlgorithmTypeAttribute>()?.AlgorithmType;
            return algorithmType != null 
                ? (IAlgorithm)Activator.CreateInstance(algorithmType, graph, endPoints) 
                : new NullAlgorithm();
        }
    }
}
