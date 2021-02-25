using Algorithm.Common;
using Algorithm.Interfaces;
using Common;
using Common.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;

using static Activating.ObjectActivator;

namespace Algorithm.Realizations
{
    public static class AlgorithmsFactory
    {       
        /// <summary>
        /// Descriptions of algorithms
        /// </summary>
        public static IEnumerable<string> AlgorithmsDescriptions => Algorithms.Keys.OrderBy(Key);

        static AlgorithmsFactory()
        {
            Algorithms = new Dictionary<string, IAlgorithm>();
        }

        public static void LoadAlgorithms(string path, string searchPattern = "*.dll",
            SearchOption searchOption = SearchOption.AllDirectories)
        {
            Algorithms =
                ClassLoader<IAlgorithm>
                .Instance
                .LoadTypesFromAssembles(path, searchPattern, searchOption)
                .Where(IsValidAlgorithm)
                .ForEach(RegisterConstructors)
                .ToDictionary(Description, Instance);
        }

        /// <summary>
        /// Returns algorithm according to 
        /// <paramref name="algorithmDescription"></paramref>
        /// </summary>
        /// <param name="algorithmDescription"></param>
        /// <param name="graph"></param>
        /// <returns>An instance of algorithm if 
        /// <paramref name="algorithmDescription"></paramref> exists and
        /// <see cref="DefaultAlgorithm"></see> when doesn't</returns>
        /// <exception cref="KeyNotFoundException">Thrown when activator 
        /// doesn't exist for algorithm with 
        /// <paramref name="algorithmDescription"></paramref> key</exception>
        public static IAlgorithm GetAlgorithm(string algorithmDescription)
        {
            return Algorithms.TryGetValue(algorithmDescription, out IAlgorithm algorithm)
                ? algorithm : new DefaultAlgorithm();
        }

        private static IDictionary<string, IAlgorithm> Algorithms { get; set; }

        private static IAlgorithm Instance(Type algorithmType) 
            => CreateInstance<IAlgorithm>(algorithmType);

        private static void RegisterConstructors(Type algorithmType) 
            => RegisterConstructors<IAlgorithm>(algorithmType);

        private static string Description(Type algorithmType)
        {
            var attribute = algorithmType.GetAttribute<DescriptionAttribute>();
            return attribute?.Description ?? algorithmType.Name;
        }

        private static bool IsValidAlgorithm(Type algorithmType) 
            => !algorithmType.IsFilterable() && !algorithmType.IsAbstract;

        private static string Key(string key) => key;
    }
}