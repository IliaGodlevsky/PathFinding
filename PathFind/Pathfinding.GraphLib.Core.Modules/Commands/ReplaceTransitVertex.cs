using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Interface.Extensions;
using Pathfinding.GraphLib.Core.Modules.Interface;
using System.Linq;

namespace Pathfinding.GraphLib.Core.Modules.Commands
{
    internal sealed class ReplaceTransitVertex<TVertex> : IPathfindingRangeCommand<TVertex>
        where TVertex : IVertex
    {
        private readonly ReplaceTransitVerticesModule<TVertex> module;

        public ReplaceTransitVertex(ReplaceTransitVerticesModule<TVertex> module)
        {
            this.module = module;
        }

        public void Execute(IPathfindingRange<TVertex> range, TVertex vertex)
        {
            var toRemove = module.TransitVerticesToReplace.First();
            module.TransitVerticesToReplace.Remove(toRemove);
            int toReplaceIndex = range.Transit.IndexOf(toRemove);
            while (toReplaceIndex == -1 && module.TransitVerticesToReplace.Count > 0)
            {
                toRemove = module.TransitVerticesToReplace.First();
                module.TransitVerticesToReplace.Remove(toRemove);
                toReplaceIndex = range.Transit.IndexOf(toRemove);
            }
            if (toReplaceIndex != -1)
            {
                range.Transit.Remove(toRemove);
                range.Transit.Insert(toReplaceIndex, vertex);
            }
        }

        public bool CanExecute(IPathfindingRange<TVertex> range, TVertex vertex)
        {
            return module.TransitVerticesToReplace.Count > 0
                && !module.TransitVerticesToReplace.Contains(vertex)
                && range.CanBeInRange(vertex);
        }
    }
}
