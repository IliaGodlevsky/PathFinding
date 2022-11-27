using Pathfinding.GraphLib.Core.Interface;
using System.Collections.Generic;

namespace Pathfinding.GraphLib.Core.Realizations.Range.Commands.Abstractions
{
    public abstract class ReplaceIntermediatesCommand<TVertex> : PathfindingRangeCommand<TVertex>
        where TVertex : IVertex
    {
        private readonly ReplaceIntermediateVerticesModule<TVertex> module;

        protected IList<TVertex> ToReplaceIntermediates => module.ToReplaceIntermediates;

        protected ReplaceIntermediatesCommand(PathfindingRange<TVertex> pathfindingRange, 
            ReplaceIntermediateVerticesModule<TVertex> module) : base(pathfindingRange)
        {
            this.module = module;
        }
    }
}
