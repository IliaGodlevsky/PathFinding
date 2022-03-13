using Common.Extensions;
using ConsoleVersion.Interface;
using ConsoleVersion.Model;
using GraphLib.Realizations.Graphs;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleVersion.Extensions
{
    internal static class GraphFieldExtensions
    {
        public static IReadOnlyCollection<IDisplayable> GenerateDisplayables(this GraphField field, Graph2D graph)
        {
            return field
                .GetAttached<IDisplayable>(graph)
                .Concat(graph.Vertices.Cast<IDisplayable>())
                .ToArray();
        }
    }
}
