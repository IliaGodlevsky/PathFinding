using Algorithm.Algorithms.Abstractions;
using Algorithm.Extensions;
using Common;
using Common.Extensions;
using GraphLib.Graphs.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using static Common.ObjectActivator;

namespace Algorithm.AlgorithmCreating
{
    public static class AlgorithmFactory
    {
        public static IEnumerable<string> AlgorithmsDescriptions { get; private set; }

        static AlgorithmFactory()
        {
            AlgorithmsDictionary = CreateAlgorithmsDictionary();
            AlgorithmsDescriptions = AlgorithmsDictionary.Keys.OrderBy(key => key);
        }

        public static IAlgorithm CreateAlgorithm(string algorithmDescription, IGraph graph)
        {
            if (AlgorithmsDictionary.TryGetValue(algorithmDescription, out Type algoType))
            {
                var activator = (Activator<IAlgorithm>)GetConstructor(algoType);
                return activator(graph);
            }
            return new DefaultAlgorithm();
        }

        private static Dictionary<string, Type> AlgorithmsDictionary { get; set; }

        private static Dictionary<string, Type> CreateAlgorithmsDictionary()
        {
            return typeof(IAlgorithm)
                .GetAssembly()
                .GetTypes()
                .Where(IsValidAlgorithm)
                .ForEach(RegisterConstructor)
                .ToDictionary(GetAlgorithmDescription);
        }

        private static void RegisterConstructor(Type type)
        {
            var ctor = type.GetConstructor(typeof(IGraph));
            RegisterConstructor<IAlgorithm>(ctor);
        }

        private static string GetAlgorithmDescription(Type algorithmType)
        {
            var attribute = algorithmType.GetAttribute<DescriptionAttribute>();
            return attribute == null ? algorithmType.ToString() : attribute.Description;
        }

        private static bool IsValidAlgorithm(Type type)
        {
            return typeof(IAlgorithm).IsAssignableFrom(type)
                && !type.IsFilterable()
                && !type.IsAbstract;
        }
    }
}