using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Interface.Extensions;
using Pathfinding.GraphLib.Core.Realizations.Range.Commands.Abstractions;
using Shared.Executable;
using Shared.Primitives.Attributes;

namespace Pathfinding.GraphLib.Core.Realizations.Range.Commands.Realizations.PathfindingRangeCommands
{
    [Order(4)]
    internal sealed class IncludeSourceVertex<TVertex> : PathfindingRangeCommand<TVertex>, IUndo
        where TVertex : IVertex
    {
        private readonly IExecutable<TVertex> excludeCommand;

        public IncludeSourceVertex(PathfindingRange<TVertex> pathfindingRange)
            : base(pathfindingRange)
        {
            excludeCommand = new ExcludeSourceVertex<TVertex>(pathfindingRange);
        }

        public override void Execute(TVertex vertex)
        {
            pathfindingRange.Source = vertex;
        }

        public override bool CanExecute(TVertex vertex)
        {
            return pathfindingRange.Source == null
                && pathfindingRange.CanBeInRange(vertex);
        }

        public void Undo()
        {
            excludeCommand.Execute(Source);
        }
    }
}