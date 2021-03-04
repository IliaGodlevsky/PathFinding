using Algorithm.Common;
using System.Reflection;
using Algorithm.Interfaces;
using Common;
using Common.Enums;
using Common.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

namespace Algorithm.Realizations
{
    public static class AlgorithmsFactory
    {
        /// <summary>
        /// Descriptions of algorithms
        /// </summary>
        public static IEnumerable<string> AlgorithmsDescriptions => algorithms.Keys.OrderBy(Key);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="searchOption"></param>
        /// <exception cref="DirectoryNotFoundException"/>
        /// <exception cref="ArgumentException"/>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ArgumentOutOfRangeException"/>
        /// <exception cref="UnauthorizedAccessException"/>
        /// <exception cref="PathTooLongException"/>
        /// <exception cref="IOException"/>
        /// <exception cref="NotSupportedException"/>
        /// <exception cref="TargetInvocationException"/>
        /// <exception cref="MethodAccessException"/>
        /// <exception cref="MemberAccessException"/>
        /// <exception cref="TypeLoadException"/>
        /// <exception cref="MissingMethodException"/>
        /// <exception cref="InvalidComObjectException"/>
        /// <exception cref="COMException"/>
        public static void LoadAlgorithms(
            string path, 
            SearchOption searchOption = SearchOption.AllDirectories)
        {
            LoadOption loadOption = LoadOption.Hierarchy;
            algorithms =
                ClassLoader<IAlgorithm>
                .Instance
                .FetchTypes(
                    path,
                    loadOption, 
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