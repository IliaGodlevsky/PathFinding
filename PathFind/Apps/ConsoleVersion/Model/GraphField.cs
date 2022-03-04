using ConsoleVersion.Extensions;
using ConsoleVersion.Interface;
using GraphLib.Interfaces;
using GraphLib.Realizations.Graphs;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleVersion.Model
{
    internal sealed class GraphField : IGraphField, IDisplayable
    {
        public IReadOnlyCollection<IVertex> Vertices { get; }

        private IReadOnlyCollection<IDisplayable> Displayables { get; }

        public GraphField(Graph2D graph)
        {
            Vertices = graph.Vertices;
            var axes = this.GetAttachedFramedAxes(graph);
            var vertices = Vertices.OfType<Vertex>();
            Displayables = axes.Concat(vertices).ToArray();
        }

        public void Display() => Displayables.DisplayAll();
    }
}