using Pathfinding.GraphLib.Core.Interface;
using Shared.Executable;

namespace Pathfinding.GraphLib.Core.Modules.Interface
{
    public interface IPathfindingRangeBuilder<TVertex> : IUndo
        where TVertex : IVertex
    {
        IPathfindingRange<TVertex> Range { get; }

        void Include(TVertex vertex);

        void Exclude(TVertex vertex);
    }
}
