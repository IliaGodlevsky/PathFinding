﻿using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Visualization.Subscriptions;
using System.Windows.Input;

namespace Pathfinding.App.WPF._2D.Model
{
    internal sealed class Wpf2DVertexReverseModule : ReverseVertexModule<Vertex>, IGraphSubscription<Vertex>
    {
        private void ReverseVertex(object sender, MouseButtonEventArgs e)
        {
            Reverse((Vertex)e.Source);
        }

        public void Subscribe(IGraph<Vertex> graph)
        {
            foreach (var vertex in graph)
            {
                vertex.MouseRightButtonDown += ReverseVertex;
            }
        }

        public void Unsubscribe(IGraph<Vertex> graph)
        {
            foreach (var vertex in graph)
            {
                vertex.MouseRightButtonDown -= ReverseVertex;
            }
        }
    }
}