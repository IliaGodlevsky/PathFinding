using Algorithm.Algorithms.Abstractions;
using Common.Extensions;
using GraphLib.Graphs.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Algorithm.AlgorithmCreating
{
    public static class AlgorithmFactory
    {
        public static IEnumerable<string> AlgorithmsDescriptions { get; private set; }

        static AlgorithmFactory()
        {
            Algorithms = GetDictionaryOfAlgorithms();
            AlgorithmsDescriptions = Algorithms.Keys.OrderBy(key => key);
        }

        public static IAlgorithm CreateAlgorithm(string algorithmKey, IGraph graph)
        {
            return Algorithms.ContainsKey(algorithmKey)
                ? (IAlgorithm)Activator.CreateInstance(Algorithms[algorithmKey], graph)
                : new DefaultAlgorithm();
        }

        private static Dictionary<string, Type> Algorithms { get; set; }

        private static Dictionary<string, Type> GetDictionaryOfAlgorithms()
        {
            return typeof(IAlgorithm)
                .GetAssembly()
                .GetTypes()
                .Except(NotActiveAlgorithms)
                .Where(IsPathfindingAlgorithmType)
                .ToDictionary(GetAlgorithmDescription);
        }

        private static string GetAlgorithmDescription(Type algorithmType)
        {
            var attribute = (DescriptionAttribute)Attribute.
                GetCustomAttribute(algorithmType, typeof(DescriptionAttribute));
            var description = attribute == null ? algorithmType.ToString() : attribute.Description;
            return description;
        }

        private static IEnumerable<Type> NotActiveAlgorithms
        {
            get
            {
                return new Type[] { typeof(DefaultAlgorithm), typeof(BaseAlgorithm) };
            }
        }

        private static bool IsPathfindingAlgorithmType(Type type)
        {
            return type.IsImplementationOf<IAlgorithm>();
        }
    }
}
