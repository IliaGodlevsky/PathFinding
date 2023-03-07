using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Model.FramedAxes;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using Pathfinding.VisualizationLib.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.App.Console.Model
{
    internal sealed class GraphField : IGraphField<Vertex>, IDisplayable
    {
        public static readonly GraphField Empty =
            new(Graph2D<Vertex>.Empty, Array.Empty<FramedAxis>());

        public IReadOnlyCollection<Vertex> Vertices { get; }

        private IReadOnlyCollection<IDisplayable> Displayables { get; }

        public GraphField(Graph2D<Vertex> graph)
            : this(graph, new FramedOverAbscissa(graph),
                          new FramedUnderAbscissa(graph),
                          new FramedToRightOrdinate(graph),
                          new FramedToLeftOrdinate(graph))
        {

        }

        private GraphField(Graph2D<Vertex> graph, params FramedAxis[] axes)
        {
            Vertices = graph;
            Displayables = axes.Concat<IDisplayable>(graph).ToArray();
        }

        public void Display()
        {
            using (Cursor.HideCursor())
            {
                Displayables.Display();
            }
        }
    }
}