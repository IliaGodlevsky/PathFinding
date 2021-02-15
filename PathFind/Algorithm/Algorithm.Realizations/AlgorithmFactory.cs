using Algorithm.Common;
using Algorithm.Interfaces;
using Common.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;

using static Activating.ObjectActivator;

namespace Algorithm.Realizations
{
    public static class AlgorithmsPluginLoader
    {
        /// <summary>
        /// Descriptions of algorithms
        /// </summary>
        public static IEnumerable<string> AlgorithmsDescriptions => Algorithms.Keys.OrderBy(Key);

        static AlgorithmsPluginLoader()
        {
            Algorithms = new Dictionary<string, IAlgorithm>();
        }

        public static void LoadAlgorithms(string path)
        {
            if (Directory.Exists(path))
            {
                Algorithms = Directory
                    .GetFiles(path, "*.dll", SearchOption.AllDirectories)
                    .Select(Assembly.LoadFrom)
                    .SelectMany(assembly => assembly.GetTypes().Where(IsValidAlgorithm))
                    .DistinctBy(type => type.FullName)
                    .ForEach(RegisterConstructor)
                    .ToDictionary(GetAlgorithmDescription, GetInstance);
            }
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
                ? algorithm : new DefaultAlgorithm();
        }

        private static IDictionary<string, IAlgorithm> Algorithms { get; set; }

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