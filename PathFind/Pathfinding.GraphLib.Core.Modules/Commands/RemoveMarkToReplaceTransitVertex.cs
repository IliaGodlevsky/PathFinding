using Pathfinding.GraphLib.Core.Interface;

namespace Pathfinding.GraphLib.Core.Modules.Commands
{
    internal sealed class RemoveMarkToReplaceTransitVertex<TVertex> : ReplaceTransitCommand<TVertex>
        where TVertex : IVertex
    {
        public RemoveMarkToReplaceTransitVertex(ReplaceTransitVerticesModule<TVertex> module)
            : base(module)
        {

        }

        public override void Execute(TVertex vertex)
        {
            module.TransitVerticesToReplace.Remove(vertex);
        }

        public override bool CanExecute(IPathfindingRange<TVertex> range, TVertex vertex)
        {
            return module.TransitVerticesToReplace.Contains(vertex);
        }
    }
}
