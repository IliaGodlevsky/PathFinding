using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.VisualizationLib.Core.Interface;

namespace Pathfinding.GraphLib.Visualization.Commands.Realizations.PathfindingRangeCommands
{
    internal sealed class RestoreSourceVisual<TVertex> : IVisualCommand<TVertex>
        where TVertex : IVertex, ITotallyVisualizable
    {
        public void Execute(TVertex vertex)
        {
            vertex.VisualizeAsSource();
        }

        public bool CanExecute(IPathfindingRange<TVertex> range, TVertex vertex)
        {
            return vertex.Equals(range.Source);
        }
    }
}