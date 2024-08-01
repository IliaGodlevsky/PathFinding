using Pathfinding.Domain.Interface;
using Shared.Executable;

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
