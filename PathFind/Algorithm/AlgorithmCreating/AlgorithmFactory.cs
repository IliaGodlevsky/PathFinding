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

        public static IAlgorithm CreateAlgorithm(string algorithmKey, IGraph graph)
        {
            if (AlgorithmsDictionary.TryGetValue(algorithmKey, out Type algoType))
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
                .Except(typeof(DefaultAlgorithm), typeof(BaseAlgorithm))
                .Where(IsPathfindingAlgorithmType)
                .ToDictionary(GetAlgorithmDescription)
                .ForEach(RegisterConstructor);
        }

        private static void RegisterConstructor(Type type)
        {
            var ctor = type.GetConstructor(typeof(IGraph));
            RegisterConstructor<IAlgorithm>(ctor);
        }

        private static string GetAlgorithmDescription(Type algorithmType)
        {
            var attribute = (DescriptionAttribute)Attribute.
                GetCustomAttribute(algorithmType, typeof(DescriptionAttribute));
            var description = attribute == null ? algorithmType.ToString() : attribute.Description;
            return description;
        }

        private static bool IsPathfindingAlgorithmType(Type type)
        {
            return type.IsImplementationOf<IAlgorithm>();
        }
    }
}