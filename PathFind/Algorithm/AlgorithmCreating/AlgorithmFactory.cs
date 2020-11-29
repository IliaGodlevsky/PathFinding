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
        public static ICollection<string> AlgorithmKeys => Algorithms.Keys;

        public static IDictionary<string, Type> Algorithms { get; private set; }

        static AlgorithmFactory()
        {
            Algorithms = new Dictionary<string, Type>();

            var algorithmInterfaceType = typeof(IPathFindingAlgorithm);
            var assembly = Assembly.Load(algorithmInterfaceType.Assembly.GetName());
            var assemblyTypes = assembly.GetTypes().
                Where(type => type != typeof(DefaultAlgorithm));

            foreach (var type in assemblyTypes)
            {
                var interfaces = type.GetInterfaces();
                var interfacesNames = interfaces.Select(interf => interf.Name);

                if (interfacesNames.Contains(algorithmInterfaceType.Name))
                {
                    dynamic attribute = Attribute.GetCustomAttribute(type, typeof(DescriptionAttribute));
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
