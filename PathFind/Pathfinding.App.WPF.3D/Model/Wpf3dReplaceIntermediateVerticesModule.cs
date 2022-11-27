using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Realizations.Range;
using System.Windows.Input;

namespace Pathfinding.App.WPF._3D.Model
{
    internal sealed class Wpf3dReplaceIntermediateVerticesModule : ReplaceIntermediateVerticesModule<Vertex3D>, IGraphSubscription<Vertex3D>
    {
        public Wpf3dReplaceIntermediateVerticesModule(PathfindingRange<Vertex3D> pathfindingRange)
            : base(pathfindingRange)
        {
        }

        private void MarkAsIntermediateToRemove(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Middle)
            {
                MarkIntermediateVertexToReplace((Vertex3D)e.Source);
            }
        }

        public void Subscribe(IGraph<Vertex3D> graph)
        {
            foreach (var vertex in graph)
            {
                vertex.MouseUp += MarkAsIntermediateToRemove;
            }
        }

        public void Unsubscribe(IGraph<Vertex3D> graph)
        {
            foreach (var vertex in graph)
            {
                vertex.MouseUp -= MarkAsIntermediateToRemove;
            }
        }
    }
}
