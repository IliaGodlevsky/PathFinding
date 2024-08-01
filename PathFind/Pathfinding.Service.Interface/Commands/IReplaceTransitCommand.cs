using Pathfinding.Domain.Interface;

namespace Pathfinding.Service.Interface.Commands
{
    public interface IReplaceTransitCommand<TVertex>
        where TVertex : IVertex
    {
        void Execute(TVertex vertex);

        bool CanExecute(IPathfindingRange<TVertex> range, TVertex vertex);
    }
}
