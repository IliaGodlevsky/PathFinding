using Algorithm.PathFindingAlgorithms;
using Algorithm.PathFindingAlgorithms.Interface;
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
        public static IEnumerable<string> AlgorithmKeys => Algorithms.Keys;

        public static IDictionary<string, Type> Algorithms { get; private set; }

        static AlgorithmFactory()
        {
            Algorithms = new Dictionary<string, Type>();
            var algorithmInterfaceType = typeof(IPathFindingAlgorithm);
            var filterAlgorithmType = typeof(DefaultAlgorithm);
            var assembly = Assembly.Load(algorithmInterfaceType.Assembly.GetName());
            var assemblyTypes = assembly.GetTypes().Where(type => type != filterAlgorithmType);

            foreach (var type in assemblyTypes)
            {
                var typeRealizedInterfaces = type.GetInterfaces().Select(interf => interf.Name);

                if (typeRealizedInterfaces.Contains(algorithmInterfaceType.Name))
                {
                    var attributeType = typeof(DescriptionAttribute);
                    var attribute = (DescriptionAttribute)Attribute.GetCustomAttribute(type, attributeType);
                    var description = attribute != null ? attribute.Description : type.ToString();
                    Algorithms.Add(description, type);
                }
            }
        }

        public static IPathFindingAlgorithm CreateAlgorithm(string algorithmKey, IGraph graph)
        {
            return Algorithms.ContainsKey(algorithmKey)
                ? (IPathFindingAlgorithm)Activator.CreateInstance(Algorithms[algorithmKey], graph)
                : new DefaultAlgorithm();
        }
    }
}
