using Algorithm.Algorithms.Abstractions;
using Common.Extensions;
using GraphLib.Graphs.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Algorithm.AlgorithmCreating
{
    public static class AlgorithmFactory
    {
        public static IEnumerable<string> GetAlgorithmKeys()
        {
            return AlgorithmKeys;
        }

        static AlgorithmFactory()
        {
            Algorithms = AlgorithmsTypes.ToDictionary(type => GetAlgorithmDescription(type), type => type);
            AlgorithmKeys = Algorithms.Keys.OrderBy(key => key);
        }

        public static IAlgorithm CreateAlgorithm(string algorithmKey, IGraph graph)
        {
            return Algorithms.ContainsKey(algorithmKey)
                ? (IAlgorithm)Activator.CreateInstance(Algorithms[algorithmKey], graph)
                : new DefaultAlgorithm();
        }

        private static IEnumerable<string> AlgorithmKeys { get; set; }

        private static Dictionary<string, Type> Algorithms { get; set; }

        private static IEnumerable<Type> AlgorithmsTypes 
            => AssemblyTypes.Where(type => type.GetInterfacesNames().Contains(AlgorithmInterfaceName));

        private static string GetAlgorithmDescription(Type algorithmType)
        {
            var attribute = (DescriptionAttribute)Attribute.
                GetCustomAttribute(algorithmType, typeof(DescriptionAttribute));
            var description = attribute == null ? algorithmType.ToString() : attribute.Description;
            return description;
        }

        private static Type AlgorithmInterfaceType => typeof(IAlgorithm);

        private static string AlgorithmInterfaceName => AlgorithmInterfaceType.Name;

        private static Assembly AlgorithmInterfaceAssembly => AlgorithmInterfaceType.GetAssembly();

        private static IEnumerable<Type> FilterTypes => new Type[] { typeof(DefaultAlgorithm), typeof(BaseAlgorithm) };

        private static IEnumerable<Type> AssemblyTypes
            => AlgorithmInterfaceAssembly.GetTypes().Where(type => !FilterTypes.Contains(type));
    }
}
