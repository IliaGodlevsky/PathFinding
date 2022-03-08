using Common.Extensions;
using ConsoleVersion.Interface;
using ConsoleVersion.Model;
using ConsoleVersion.Model.FramedAxes;
using GraphLib.Realizations.Graphs;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleVersion.Extensions
{
    internal static class GraphFieldExtensions
    {
        public static IReadOnlyCollection<IDisplayable> GenerateDisplayables(this GraphField field, Graph2D graph)
        {
            return field.GetAttached<FramedAxis>(graph).Concat(graph.Vertices.OfType<IDisplayable>()).ToArray();
        }
    }
}
