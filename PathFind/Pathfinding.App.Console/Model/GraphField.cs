using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Model.FramedAxes;
using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Realizations;
using Pathfinding.VisualizationLib.Core.Interface;
using Shared.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.App.Console.Model
{
    internal sealed class GraphField : IGraphField<Vertex>, IDisplayable
    {
        public static readonly GraphField Empty = new(Graph<Vertex>.Empty);

        IReadOnlyCollection<Vertex> IGraphField<Vertex>.Vertices => Vertices;

        public IGraph<Vertex> Vertices { get; }

        private IDisplayable[] Displayables { get; }

        public GraphField(IGraph<Vertex> graph)
            : this(graph, new FramedOverAbscissa(graph),
                          new FramedUnderAbscissa(graph),
                          new FramedToRightOrdinate(graph),
                          new FramedToLeftOrdinate(graph))
        {

        }

        private GraphField(IGraph<Vertex> graph, params FramedAxis[] axes)
        {
            Vertices = graph;
            Displayables = axes.Concat<IDisplayable>(graph).ToArray();
        }

        public void Display()
        {
            using (Cursor.HideCursor())
            {
                Displayables.ForEach(d => d.Display());
            }
        }
    }
}