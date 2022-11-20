using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.VisualizationLib.Core.Interface;

namespace Pathfinding.GraphLib.Visualization.Subscriptions.Commands
{
    internal sealed class SetVertexAsObstacleCommand<TVertex> : IVisualizationCommand<TVertex>
        where TVertex : IVertex, IVisualizable
    {
        public void Execute(TVertex vertex)
        {
            vertex.IsObstacle = true;
            vertex.VisualizeAsObstacle();
        }

        public bool CanExecute(TVertex vertex)
        {
            return !vertex.IsObstacle && !vertex.IsVisualizedAsPathfindingRange();
        }
    }
}
