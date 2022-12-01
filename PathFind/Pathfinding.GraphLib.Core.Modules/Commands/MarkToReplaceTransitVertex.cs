using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Modules.Interface;
using Shared.Executable;
using Shared.Extensions;
using System.Linq;

namespace Pathfinding.GraphLib.Core.Modules.Commands
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
            foreach (var vertex in module.TransitVerticesToReplace.ToArray())
            {
                undoCommand.Execute(vertex);
            }
        }
    }
}
