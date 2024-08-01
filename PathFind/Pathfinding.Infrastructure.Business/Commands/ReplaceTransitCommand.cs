using Pathfinding.Domain.Interface;
using Pathfinding.Service.Interface.Commands;

namespace Pathfinding.Infrastructure.Business.Commands
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
