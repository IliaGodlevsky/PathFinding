using Algorithm.Base;
using Algorithm.Common;
using Algorithm.Common.Exceptions;
using Algorithm.Extensions;
using Algorithm.Interfaces;
using Common;
using Common.Enums;
using Common.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using GraphLib.Interfaces;

namespace Algorithm.Realizations
{
    public static class AlgorithmsFactory
    {
        /// <summary>
        /// Descriptions of algorithms
        /// </summary>
        public static IEnumerable<string> AlgorithmsDescriptions => algorithmsTypes.Keys.OrderBy(Key);

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
        /// <exception cref="NoAlgorithmsLoadedException"/>
        public static void LoadAlgorithms(string path,
            SearchOption searchOption = SearchOption.AllDirectories)
        {
            algorithmsTypes = ClassLoader<IAlgorithm>
                .Instance
                .FetchTypes(path, LoadOption.Hierarchy, searchOption)
                .Where(IsConcreteType)
                .ToDictionary(AlgorithmDescription)
                .CheckForEmptiness();
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
        public static IAlgorithm GetAlgorithm(string key, IGraph graph)
        {
            return algorithmsTypes.TryGetValue(key, out var type)
                ? (IAlgorithm)Activator.CreateInstance(type, graph) 
                : BaseAlgorithm.Default;
        }

        static AlgorithmsFactory()
        {
            algorithmsTypes = new Dictionary<string, Type>();
        }

        private static string AlgorithmDescription(Type type)
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

        private static Dictionary<string, Type> algorithmsTypes;
    }
}