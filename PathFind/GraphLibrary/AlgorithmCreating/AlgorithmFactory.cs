using GraphLibrary.Graphs.Interface;
using GraphLibrary.PathFindingAlgorithm;
using GraphLibrary.PathFindingAlgorithm.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace GraphLibrary.AlgorithmCreating
{
    public static class AlgorithmFactory
    {
        public static IEnumerable<string> AlgorithmKeys => Algorithms.Keys;
        public static IDictionary<string, Type> Algorithms { get; private set; }

        static AlgorithmFactory()
        {
            Algorithms = new Dictionary<string, Type>();
            var assembly = Assembly.Load(typeof(IPathFindingAlgorithm).Assembly.GetName());
            foreach (var type in assembly.GetTypes().Where(t => t != typeof(NullAlgorithm)))
            {
                if (type.GetInterfaces().Select(interf => interf.Name).
                    Contains(typeof(IPathFindingAlgorithm).Name))
                {
                    var attribute = (DescriptionAttribute)Attribute.
                        GetCustomAttribute(type, typeof(DescriptionAttribute));
                    var description = attribute != null ? attribute.Description : type.ToString();
                    Algorithms.Add(description, type);
                }
            }
        }

        public static IPathFindingAlgorithm CreateAlgorithm(string algorithmKey, IGraph graph)
        {
            return Algorithms.Keys.Contains(algorithmKey)
                ? (IPathFindingAlgorithm)Activator.CreateInstance(Algorithms[algorithmKey], graph)
                : NullAlgorithm.Instance;
        }
    }
}
