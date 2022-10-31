using Common.Extensions.EnumerableExtensions;
using ConsoleVersion.Interface;
using ConsoleVersion.Model.FramedAxes;
using GraphLib.Interfaces;
using GraphLib.Realizations.Graphs;
using System.Collections.Generic;

namespace ConsoleVersion.Model
{
    internal sealed class GraphField : IGraphField<Vertex>, IDisplayable
    {
        public IReadOnlyCollection<Vertex> Vertices { get; }

        private IEnumerable<IDisplayable> Displayables { get; }

        public GraphField(Graph2D<Vertex> graph)
        {
            Vertices = graph;
            var displayables = new List<IDisplayable>()
            {
                new FramedOverAbscissa(graph),
                new FramedUnderAbscissa(graph),
                new FramedToRightOrdinate(graph),
                new FramedToLeftOrdinate(graph)
            };
            displayables.AddRange(Vertices);
            Displayables = displayables.AsReadOnly();
        }

        public void Display()
        {
            using (Cursor.HideCursor())
            {
                Displayables.ForEach(display => display.Display());
            }
        }
    }
}