using Pathfinding.App.WPF._3D.Interface;
using Pathfinding.App.WPF._3D.Model.Axes;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using Pathfinding.VisualizationLib.Core.Interface;
using Shared.Extensions;
using System.Collections.Generic;
using System.Windows.Media.Media3D;

namespace Pathfinding.App.WPF._3D.Model
{
    internal sealed class GraphField3D : ModelVisual3D, IGraphField<Vertex3D>
    {
        public IAxis Abscissa { get; }

        public IAxis Ordinate { get; }

        public IAxis Applicate { get; }

        public IReadOnlyCollection<Vertex3D> Vertices { get; }

        public GraphField3D(Graph3D<Vertex3D> graph)
        {
            Vertices = graph;
            Children.AddRange(Vertices);
            Abscissa = new Abscissa(graph);
            Ordinate = new Ordinate(graph);
            Applicate = new Applicate(graph);
        }
    }
}