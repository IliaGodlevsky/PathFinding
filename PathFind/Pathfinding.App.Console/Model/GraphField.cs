using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Model.FramedAxes;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using Pathfinding.VisualizationLib.Core.Interface;
using Shared.Extensions;
using System.Collections.Generic;

namespace Pathfinding.App.Console.Model
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