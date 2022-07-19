using Common.Extensions.EnumerableExtensions;
using ConsoleVersion.Interface;
using ConsoleVersion.Model.FramedAxes;
using GraphLib.Interfaces;
using GraphLib.Realizations.Graphs;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleVersion.Model
{
    internal sealed class GraphField : IGraphField, IDisplayable
    {
        public IReadOnlyCollection<Vertex> Vertices { get; }

        IReadOnlyCollection<IVertex> IGraphField.Vertices => Vertices;

        private IReadOnlyCollection<IDisplayable> Displayables { get; }

        public GraphField(Graph2D graph)
        {
            Vertices = graph.Vertices.Cast<Vertex>().ToReadOnly();
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