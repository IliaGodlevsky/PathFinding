using Algorithm.Extensions;
using Algorithm.Interface;
//using Algorithm.NullObjects;
using Common.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

using static Common.ObjectActivator;

namespace Algorithm.AlgorithmCreating
{
    public static class AlgorithmFactory
    {
        /// <summary>
        /// Descriptions of algorithms
        /// </summary>
        public static IEnumerable<string> AlgorithmsDescriptions { get; private set; }

        static AlgorithmFactory()
        {
            AlgorithmsInterface = typeof(IAlgorithm);
            Algorithms = CreateAlgorithmsDictionary();
            AlgorithmsDescriptions = Algorithms.Keys.OrderBy(Key);
        }

        /// <summary>
        /// Returns algorithm according to <paramref name="algorithmDescription"></paramref>
        /// </summary>
        /// <param name="algorithmDescription"></param>
        /// <param name="graph"></param>
        /// <returns>An instance of algorithm if <paramref name="algorithmDescription"></paramref> exists and
        /// <see cref="DefaultAlgorithm"></see> when doesn't</returns>
        /// <exception cref="KeyNotFoundException">Thrown when activator 
        /// doesn't exist for algorithm with <paramref name="algorithmDescription"></paramref> key</exception>
        public static IAlgorithm GetAlgorithm(string algorithmDescription)
        {
            return Algorithms.TryGetValue(algorithmDescription, out IAlgorithm algorithm)
                ? algorithm : null;
        }

        private static IDictionary<string, IAlgorithm> Algorithms { get; set; }

        private static Type AlgorithmsInterface { get; }

        private static IDictionary<string, IAlgorithm> CreateAlgorithmsDictionary()
        {
            return AlgorithmsInterface
                .GetAssembly()
                .GetTypes()
                .Where(IsValidAlgorithm)
                .ForEach(RegisterConstructor)
                .ToDictionary(GetAlgorithmDescription, GetInstance);
        }

        private static IAlgorithm GetInstance(Type algorithmType)
        {
            return CreateInstance<IAlgorithm>(algorithmType);
        }

        private static void RegisterConstructor(Type type)
        {
            var ctor = type.GetConstructor();
            RegisterConstructor<IAlgorithm>(ctor);
        }

        private static string GetAlgorithmDescription(Type algorithmType)
        {
            var attribute = algorithmType.GetAttribute<DescriptionAttribute>();
            return attribute?.Description ?? algorithmType.Name;
        }

        private static bool IsValidAlgorithm(Type type)
        {
            return typeof(IAlgorithm).IsAssignableFrom(type)
                && !type.IsFilterable()
                && !type.IsAbstract;
        }

        private static string Key(string key) => key;
    }
}