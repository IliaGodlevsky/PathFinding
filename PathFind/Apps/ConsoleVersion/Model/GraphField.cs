using Common.Extensions.EnumerableExtensions;
using ConsoleVersion.Extensions;
using ConsoleVersion.Interface;
using ConsoleVersion.Model.FramedAxes;
using GraphLib.Interfaces;
using GraphLib.Realizations.Graphs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ConsoleVersion.Model
{
    internal sealed class GraphField : IGraphField, IDisplayable
    {
        private IEnumerable<IDisplayable> UiElements => uiElements.Value;

        public IReadOnlyCollection<IVertex> Vertices { get; }

        public GraphField(Graph2D graph)
        {
            Vertices = graph.Vertices;
            uiElements = new Lazy<IEnumerable<IDisplayable>>(() => CreateCollection(graph));
        }

        private IEnumerable<IDisplayable> CreateCollection(Graph2D graph)
        {
            var elements = new Collection<IDisplayable>
            {
                new FramedOverAbscissa(graph.Width, graph.Length),
                new FramedUnderAbscissa(graph.Width),
                new FramedToRightOrdinate(graph.Width, graph.Length),
                new FramedToLeftOrdinate(graph.Length)
            };
            return elements.AddCastedRange(Vertices);
        }

        public void Display()
        {
            UiElements.DisplayAll();
        }

        private readonly Lazy<IEnumerable<IDisplayable>> uiElements;
    }
}