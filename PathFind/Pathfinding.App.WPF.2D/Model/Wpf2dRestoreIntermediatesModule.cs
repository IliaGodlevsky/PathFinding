using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Realizations.Range;
using Pathfinding.GraphLib.Visualization;
using System.Windows.Input;

namespace Pathfinding.App.WPF._2D.Model
{
    internal sealed class Wpf2dReplaceIntermediateVerticesModule : ReplaceIntermediateVerticesModule<Vertex>, IGraphSubscription<Vertex>
    {
        public Wpf2dReplaceIntermediateVerticesModule(PathfindingRange<Vertex> pathfindingRange) 
            : base(pathfindingRange)
        {
        }

        private void MarkAsIntermediateToRemove(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Middle)
            {
                MarkIntermediateVertexToReplace((Vertex)e.Source);
            }
        }

        public void Subscribe(IGraph<Vertex> graph)
        {
            foreach (var vertex in graph)
            {
                vertex.MouseUp += MarkAsIntermediateToRemove;
            }
        }

        public void Unsubscribe(IGraph<Vertex> graph)
        {
            foreach (var vertex in graph)
            {
                vertex.MouseUp -= MarkAsIntermediateToRemove;
            }
        }
    }
}
