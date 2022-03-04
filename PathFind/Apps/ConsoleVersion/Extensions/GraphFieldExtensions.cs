using Common.Extensions;
using ConsoleVersion.Interface;
using ConsoleVersion.Model;
using ConsoleVersion.Model.FramedAxes;
using GraphLib.Realizations.Graphs;
using System.Collections.Generic;

namespace ConsoleVersion.Extensions
{
    internal static class GraphFieldExtensions
    {
        public static IReadOnlyCollection<IDisplayable> GetAttachedFramedAxes(this GraphField field, Graph2D graph)
        {
            return field.GetAttached<FramedAxis>(graph);
        }
    }
}
