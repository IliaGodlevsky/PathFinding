using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Realizations.Range.Commands.Realizations.CommandCollections;
using Shared.Executable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pathfinding.GraphLib.Core.Realizations.Range
{
    public class ReplaceIntermediateVerticesModule<TVertex> : IUndo
        where TVertex : IVertex
    {
        private readonly PathfindingRange<TVertex> pathfindingRange;
        private readonly IntermediatesToRemoveCommands<TVertex> intermediateVerticesToRemoveCommands;

        internal protected virtual IList<TVertex> ToReplaceIntermediates { get; }

        public ReplaceIntermediateVerticesModule(PathfindingRange<TVertex> pathfindingRange) 
        {
            intermediateVerticesToRemoveCommands = new IntermediatesToRemoveCommands<TVertex>(pathfindingRange, this);
            ToReplaceIntermediates = new List<TVertex>();
        }

        public void Undo()
        {
            intermediateVerticesToRemoveCommands.Undo();
        }

        public virtual void MarkIntermediateVertexToReplace(TVertex vertex)
        {
            intermediateVerticesToRemoveCommands.Execute(vertex);
        }
    }
}
