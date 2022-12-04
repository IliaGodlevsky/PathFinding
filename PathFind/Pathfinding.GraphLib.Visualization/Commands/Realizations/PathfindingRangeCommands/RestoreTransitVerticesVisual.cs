using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.VisualizationLib.Core.Interface;

namespace Pathfinding.GraphLib.Visualization.Commands.Realizations.PathfindingRangeCommands
{
    internal sealed class RestoreTransitVerticesVisual<TVertex> : IVisualCommand<TVertex>
        where TVertex : IVertex, IVisualizable
    {

        public void Execute(TVertex vertex)
        {
            vertex.VisualizeAsTransit();
        }

        public  bool CanExecute(IPathfindingRange<TVertex> range, TVertex vertex)
        {
            return range.Transit.Contains(vertex);
        }
    }
}