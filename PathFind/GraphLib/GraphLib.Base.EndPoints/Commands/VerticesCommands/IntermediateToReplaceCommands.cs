using Commands.Interfaces;
using GraphLib.Base.EndPoints.Commands.EndPointsCommands;
using GraphLib.Base.EndPoints.Commands.UndoCommands;
using GraphLib.Interfaces;
using System.Collections.Generic;

namespace GraphLib.Base.EndPoints.Commands.VerticesCommands
{
    internal sealed class IntermediateToReplaceCommands<TVertex> : BaseVerticesCommands<TVertex>
        where TVertex : IVertex, IVisualizable
    {
        public IntermediateToReplaceCommands(BaseEndPoints<TVertex> endPoints) : base(endPoints)
        {
        }

        protected override IEnumerable<IVertexCommand<TVertex>> GetCommands(BaseEndPoints<TVertex> endPoints)
        {
            yield return new CancelMarkToReplaceCommand<TVertex>(endPoints);
            yield return new MarkToReplaceCommand<TVertex>(endPoints);
        }

        protected override IEnumerable<IUndoCommand> GetUndoCommands(BaseEndPoints<TVertex> endPoints)
        {
            yield return new UndoMarkToReplaceCommand<TVertex>(endPoints);
        }
    }
}