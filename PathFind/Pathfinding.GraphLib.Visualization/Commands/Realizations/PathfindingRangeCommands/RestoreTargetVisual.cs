using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.VisualizationLib.Core.Interface;

namespace Pathfinding.GraphLib.Visualization.Commands.Realizations.PathfindingRangeCommands
{
    internal sealed class RestoreTargetVisual<TVertex> : IVisualCommand<TVertex>
        where TVertex : IVertex, ITotallyVisualizable
    {
        public void Execute(TVertex vertex)
        {
            vertex.VisualizeAsTarget();
        }

        public bool CanExecute(IPathfindingRange<TVertex> range, TVertex vertex)
        {
            return vertex.Equals(range.Target);
        }
    }
}