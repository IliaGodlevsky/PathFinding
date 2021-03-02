using Algorithm.Common;
using Algorithm.Interfaces;
using Common;
using Common.Enums;
using Common.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;

namespace Algorithm.Realizations
{
    public static class AlgorithmsFactory
    {
        /// <summary>
        /// Descriptions of algorithms
        /// </summary>
        public static IEnumerable<string> GetAlgorithmsDescriptions()
        {
            return algorithms.Keys.OrderBy(Key);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="searchPattern"></param>
        /// <param name="searchOption"></param>
        public static void LoadAlgorithms(
            string path, 
            string searchPattern = "*.dll",
            SearchOption searchOption = SearchOption.AllDirectories)
        {
            var loadOption = LoadOption.Hierarchy;
            algorithms =
                ClassLoader<IAlgorithm>
                .Instance
                .FetchTypesFromAssembles(
                    path,
                    loadOption, 
                    searchPattern, 
                    searchOption)
                .Where(IsConcreteType)
                .ToDictionary(Description, Instance);
        }

        /// <summary>
        /// Returns algorithm according to 
        /// <paramref name="key"></paramref>
        /// </summary>
        /// <param name="key"></param>
        /// <param name="graph"></param>
        /// <returns>An instance of algorithm if 
        /// <paramref name="key"></paramref> exists and
        /// <see cref="DefaultAlgorithm"></see> when doesn't</returns>
        /// <exception cref="KeyNotFoundException">Thrown when activator 
        /// doesn't exist for algorithm with 
        /// <paramref name="key"></paramref> key</exception>
        public static IAlgorithm GetAlgorithm(string key)
        {
            return algorithms.TryGetValue(key, out IAlgorithm algorithm)
                ? algorithm : new DefaultAlgorithm();
        }

        static AlgorithmsFactory()
        {
            algorithms = new Dictionary<string, IAlgorithm>();
        }

        private static IAlgorithm Instance(Type type)
        {
            return (IAlgorithm)Activator.CreateInstance(type);
        }

        private static string Description(Type type)
        {
            var attribute = type.GetAttribute<DescriptionAttribute>();
            return attribute?.Description ?? type.Name;
        }

        private static bool IsConcreteType(Type type)
        {
            return !type.IsFilterable() && !type.IsAbstract;
        }

        private static string Key(string key)
        {
            return key;
        }

        private static Dictionary<string, IAlgorithm> algorithms;
    }
}