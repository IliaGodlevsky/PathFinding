using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Model.FramedAxes;
using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Realizations;
using Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.App.Console.Model
{
    internal sealed class GraphField : IDisplayable
    {
        public static readonly GraphField Empty
            = new(Graph<Vertex>.Empty, Array.Empty<FramedAxis>());

        private IReadOnlyCollection<IDisplayable> Displayables { get; }

        public GraphField(IGraph<Vertex> graph)
            : this(graph, new FramedOverAbscissa(graph),
                          new FramedUnderAbscissa(graph),
                          new FramedToRightOrdinate(graph),
                          new FramedToLeftOrdinate(graph))
        {

        }

        private GraphField(IGraph<Vertex> graph, params FramedAxis[] axes)
        {
            Displayables = graph.Concat<IDisplayable>(axes).ToReadOnly();
        }

        public void Display()
        {
            Displayables.ForEach(element => element.Display());
        }
    }
}