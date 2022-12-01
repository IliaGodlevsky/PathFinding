using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Modules.Interface;

namespace Pathfinding.GraphLib.Core.Modules.Commands
{
    public abstract class ReplaceTransitCommand<TVertex> : IReplaceTransitCommand<TVertex>
        where TVertex : IVertex
    {
        protected readonly ReplaceTransitVerticesModule<TVertex> module;

        protected ReplaceTransitCommand(ReplaceTransitVerticesModule<TVertex> module)
        {
            this.module = module;
        }

        public abstract void Execute(TVertex vertex);

        public abstract bool CanExecute(IPathfindingRange<TVertex> range, TVertex vertex);
    }
}
