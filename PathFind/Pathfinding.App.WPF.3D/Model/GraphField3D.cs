using Pathfinding.App.WPF._3D.Model.Axes;
using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Realizations;
using Pathfinding.VisualizationLib.Core.Interface;
using Shared.Extensions;
using System.Collections.Generic;
using System.Windows.Media.Media3D;

namespace Pathfinding.App.WPF._3D.Model
{
    internal sealed class GraphField3D : ModelVisual3D, IGraphField<Vertex3D>
    {
        public static readonly GraphField3D Empty
            = new GraphField3D(Graph<Vertex3D>.Empty);

        public Abscissa Abscissa { get; }

        public Ordinate Ordinate { get; }

        public Applicate Applicate { get; }

        public IReadOnlyCollection<Vertex3D> Vertices { get; }

        public GraphField3D(IGraph<Vertex3D> graph)
        {
            Vertices = graph;
            Vertices.ForEach(Children.Add);
            Abscissa = new(graph);
            Ordinate = new(graph);
            Applicate = new(graph);
        }
    }
}