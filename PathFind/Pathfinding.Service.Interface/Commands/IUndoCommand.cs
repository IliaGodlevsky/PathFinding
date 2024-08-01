using Pathfinding.Domain.Interface;

namespace Pathfinding.Service.Interface.Commands
{
    public interface IUndoCommand<TVertex>
        where TVertex : IVertex
    {
        void Undo(IPathfindingRange<TVertex> range);
    }
}
