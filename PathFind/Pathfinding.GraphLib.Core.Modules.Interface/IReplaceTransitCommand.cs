using Pathfinding.GraphLib.Core.Interface;

namespace Pathfinding.GraphLib.Core.Modules.Interface
{
    public interface IReplaceTransitCommand<TVertex>
        where TVertex : IVertex
    {
        void Execute(TVertex vertex);

        bool CanExecute(IPathfindingRange<TVertex> range, TVertex vertex);
    }
}
