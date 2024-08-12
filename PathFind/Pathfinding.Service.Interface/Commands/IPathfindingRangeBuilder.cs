using Pathfinding.Domain.Interface;
using Pathfinding.Shared.Interface;

namespace Pathfinding.Service.Interface.Commands
{
    public interface IPathfindingRangeBuilder<TVertex> : IUndo
        where TVertex : IVertex
    {
        IPathfindingRange<TVertex> Range { get; }

        void Include(TVertex vertex);

        void Exclude(TVertex vertex);
    }
}
