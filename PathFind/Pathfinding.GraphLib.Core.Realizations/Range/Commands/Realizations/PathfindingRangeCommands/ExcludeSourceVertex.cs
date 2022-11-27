using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Realizations.Range.Commands.Abstractions;
using Shared.Primitives.Attributes;

namespace Pathfinding.GraphLib.Core.Realizations.Range.Commands.Realizations.PathfindingRangeCommands
{
    [Order(0)]
    internal sealed class ExcludeSourceVertex<TVertex> : PathfindingRangeCommand<TVertex>
        where TVertex : IVertex
    {
        public ExcludeSourceVertex(PathfindingRange<TVertex> pathfindingRange)
            : base(pathfindingRange)
        {

        }

        public override void Execute(TVertex vertex)
        {
            pathfindingRange.Source = default;
        }

        public override bool CanExecute(TVertex vertex)
        {
            return pathfindingRange.Source?.Equals(vertex) == true;
        }
    }
}