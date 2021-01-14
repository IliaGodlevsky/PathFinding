﻿using Algorithm.Algorithms.Abstractions;
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
        /// <summary>
        /// Descriptions of algorithms in alphabeth order
        /// </summary>
        public static IEnumerable<string> AlgorithmsDescriptions { get; private set; }

        static AlgorithmFactory()
        {
            AlgorithmsInterface = typeof(IAlgorithm);
            Algorithms = CreateAlgorithmsDictionary();
            AlgorithmsDescriptions = Algorithms.Keys.OrderBy(key => key);
        }

        /// <summary>
        /// Creates algorithm according to <paramref name="algorithmDescription"></paramref>
        /// </summary>
        /// <param name="algorithmDescription"></param>
        /// <param name="graph"></param>
        /// <returns>An instance of algorithm if <paramref name="algorithmDescription"></paramref> exists and
        /// <see cref="DefaultAlgorithm"></see> when doesn't</returns>
        /// <exception cref="KeyNotFoundException">Thrown when activator 
        /// doesn't exist for algorithm with <paramref name="algorithmDescription"></paramref> key</exception>
        public static IAlgorithm CreateAlgorithm(string algorithmDescription, IGraph graph)
        {
            if (Algorithms.TryGetValue(algorithmDescription, out Type algoType))
            {
                var activator = (ActivatorHandler<IAlgorithm>)GetActivator(algoType);
                return activator(graph);
            }
            return new DefaultAlgorithm();
        }

        private static Dictionary<string, Type> Algorithms { get; set; }

        private static Type AlgorithmsInterface { get; set; }

        private static Dictionary<string, Type> CreateAlgorithmsDictionary()
        {
            return AlgorithmsInterface
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
            return attribute?.Description ?? algorithmType.Name;
        }

        private static bool IsValidAlgorithm(Type type)
        {
            return typeof(IAlgorithm).IsAssignableFrom(type)
                && !type.IsFilterable()
                && !type.IsAbstract;
        }
    }
}