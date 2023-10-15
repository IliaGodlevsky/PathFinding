using Pathfinding.GraphLib.Core.Interface.Extensions;
using Pathfinding.VisualizationLib.Core.Interface;
using Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Windows.Controls;
using static Pathfinding.App.WPF._2D.Constants;

namespace Pathfinding.App.WPF._2D.Model
{
    internal sealed class GraphField : Canvas, IGraphField<Vertex>
    {
        public IReadOnlyCollection<Vertex> Vertices { get; }

        public GraphField(IReadOnlyCollection<Vertex> graph)
        {
            Vertices = graph;
            Vertices.ForEach(vertex => Locate(vertex));
        }

        public GraphField() : this(Array.Empty<Vertex>())
        {

        }

        private void Locate(Vertex vertex)
        {
            var position = vertex.Position;
            Children.Add(vertex);
            SetLeft(vertex, (DistanceBetweenVertices + vertex.Width) * position.GetX());
            SetTop(vertex, (DistanceBetweenVertices + vertex.Height) * position.GetY());
        }
    }
}
