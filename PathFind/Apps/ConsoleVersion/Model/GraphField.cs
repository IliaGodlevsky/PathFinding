using ConsoleVersion.Extensions;
using ConsoleVersion.Interface;
using GraphLib.Interfaces;
using GraphLib.Realizations.Graphs;
using System.Collections.Generic;

namespace ConsoleVersion.Model
{
    internal sealed class GraphField : IGraphField, IDisplayable
    {
        public IReadOnlyCollection<IVertex> Vertices { get; }

        private IReadOnlyCollection<IDisplayable> Displayables { get; }

        public GraphField(Graph2D graph)
        {
            Vertices = graph.Vertices;
            Displayables = this.GenerateDisplayables(graph);
        }

        public void Display() => Displayables.DisplayAll();
    }
}