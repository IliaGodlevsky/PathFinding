using Pathfinding.GraphLib.Core.Interface;

namespace Pathfinding.GraphLib.Core.Modules.Interface
{
    public interface IUndoCommand<TVertex>
        where TVertex : IVertex
    {
        void Undo(IPathfindingRange<TVertex> range);
    }
}
