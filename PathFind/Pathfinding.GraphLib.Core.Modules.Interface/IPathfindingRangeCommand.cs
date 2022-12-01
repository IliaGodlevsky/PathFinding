using Pathfinding.GraphLib.Core.Interface;

namespace Pathfinding.GraphLib.Core.Modules.Interface
{
    public interface IPathfindingRangeCommand<TVertex>
        where TVertex : IVertex
    {
        void Execute(IPathfindingRange<TVertex> range, TVertex vertex);

        bool CanExecute(IPathfindingRange<TVertex> range, TVertex vertex);
    }
}
