using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Modules;
using System.Windows.Input;

namespace Pathfinding.App.WPF._2D.Model
{
    internal sealed class ReplaceTransitVerticesSubscribtion : IGraphSubscription<Vertex>
    {
        private readonly ReplaceTransitVerticesModule<Vertex> module;

        public ReplaceTransitVerticesSubscribtion(ReplaceTransitVerticesModule<Vertex> module)
        {
            this.module = module;
        }

        private void MarkVertex(object sender, MouseButtonEventArgs e)
        {
            module.MarkTransitVertex((Vertex)e.Source);
        }

        private void ReplaceVertex(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Middle)
            {
                module.ReplaceTransitWith((Vertex)e.Source);
            }
        }

        public void Subscribe(IGraph<Vertex> graph)
        {
            foreach (var vertex in graph)
            {
                vertex.MouseDoubleClick += MarkVertex;
                vertex.MouseDown += ReplaceVertex;
            }
        }

        public void Unsubscribe(IGraph<Vertex> graph)
        {
            foreach (var vertex in graph)
            {
                vertex.MouseDoubleClick -= MarkVertex;
                vertex.MouseDown -= ReplaceVertex;
            }
        }
    }
}
