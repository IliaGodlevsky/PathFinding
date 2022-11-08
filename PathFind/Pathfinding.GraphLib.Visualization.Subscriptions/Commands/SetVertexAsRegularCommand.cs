using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.VisualizationLib.Core.Interface;

namespace Pathfinding.GraphLib.Visualization.Subscriptions.Commands
{
    internal sealed class SetVertexAsRegularCommand<TVertex> : IVisualizationCommand<TVertex>
        where TVertex : IVertex, IVisualizable
    {
        public void Execute(TVertex vertex)
        {
            vertex.IsObstacle = false;
            vertex.VisualizeAsRegular();
        }

        public bool CanExecute(TVertex vertex)
        {
            return vertex.IsObstacle && !vertex.IsVisualizedAsEndPoint;
        }
    }
}
