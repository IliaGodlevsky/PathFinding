using Pathfinding.Domain.Interface;
using Pathfinding.Service.Interface.Commands;
using Pathfinding.Shared.Extensions;
using Pathfinding.Shared.Interface;
using System.Linq;

namespace Pathfinding.Infrastructure.Business.Commands
{
    internal sealed class MarkToReplaceTransitVertex<TVertex> : ReplaceTransitCommand<TVertex>, IUndo
        where TVertex : IVertex
    {
        private readonly IReplaceTransitCommand<TVertex> undoCommand;

        public MarkToReplaceTransitVertex(ReplaceTransitVerticesModule<TVertex> module)
            : base(module)
        {
            undoCommand = new RemoveMarkToReplaceTransitVertex<TVertex>(module);
        }

        public override void Execute(TVertex vertex)
        {
            module.TransitVerticesToReplace.Add(vertex);
        }

        public override bool CanExecute(IPathfindingRange<TVertex> range, TVertex vertex)
        {
            return !vertex.IsOneOf(range.Source, range.Target)
                && range.Transit.Contains(vertex)
                && !module.TransitVerticesToReplace.Contains(vertex);
        }

        public void Undo()
        {
            module.TransitVerticesToReplace.ToArray().ForEach(undoCommand.Execute);
        }
    }
}
