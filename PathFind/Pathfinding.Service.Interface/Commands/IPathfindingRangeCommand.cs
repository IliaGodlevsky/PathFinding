using Pathfinding.Domain.Interface;
using System.Collections.Generic;

namespace Pathfinding.Service.Interface.Commands
{
    public interface IPathfindingRangeCommand<TVertex>
        where TVertex : IVertex
    {
        void Execute(IPathfindingRange<TVertex> range, TVertex vertex);

        bool CanExecute(IPathfindingRange<TVertex> range, TVertex vertex);
    }
}
