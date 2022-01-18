using Common.Extensions.EnumerableExtensions;
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
        private IReadOnlyCollection<IDisplayable> UiElements => uiElements.Value;

        public IReadOnlyCollection<IVertex> Vertices { get; }

        public GraphField(Graph2D graph)
        {
            Vertices = graph.Vertices;
            uiElements = new Lazy<IReadOnlyCollection<IDisplayable>>(() => CreateCollection(graph));
        }

        private IReadOnlyCollection<IDisplayable> CreateCollection(Graph2D graph)
        {
            var elements = new Collection<IDisplayable>
            {
                new FramedOverAbscissa(graph.Width, graph.Length),
                new FramedUnderAbscissa(graph.Width),
                new FramedToRightOrdinate(graph.Width, graph.Length),
                new FramedToLeftOrdinate(graph.Length)
            };
            return (IReadOnlyCollection<IDisplayable>)elements.AddCastedRange(Vertices);
        }

        public void Display()
        {
            UiElements.ForEach(element => element.Display());
        }

        private readonly Lazy<IReadOnlyCollection<IDisplayable>> uiElements;
    }
}