using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Modules;
using Pathfinding.GraphLib.Core.Modules.Interface;
using System.Windows.Input;

namespace Pathfinding.App.WPF._2D.Model
{
    internal sealed class ReplaceTransitVerticesSubscribtion : IGraphSubscription<Vertex>
    {
        private readonly ReplaceTransitVerticesModule<Vertex> module;
        private readonly IPathfindingRangeBuilder<Vertex> builder;

        public ReplaceTransitVerticesSubscribtion(ReplaceTransitVerticesModule<Vertex> module, IPathfindingRangeBuilder<Vertex> rangeBuilder)
        {
            this.module = module;
            this.builder = rangeBuilder;
        }

        private void MarkVertex(object sender, MouseButtonEventArgs e)
        {
            module.MarkTransitVertex(builder.Range, (Vertex)e.Source);
        }

        private void ReplaceVertex(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Middle)
            {
                module.ReplaceTransitWith(builder.Range, (Vertex)e.Source);
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
