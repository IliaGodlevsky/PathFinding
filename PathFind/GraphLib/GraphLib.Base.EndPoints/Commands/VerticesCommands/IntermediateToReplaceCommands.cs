using Commands.Interfaces;
using GraphLib.Base.EndPoints.Commands.EndPointsCommands;
using GraphLib.Base.EndPoints.Commands.UndoCommands;
using GraphLib.Interfaces;
using System.Collections.Generic;

namespace GraphLib.Base.EndPoints.Commands.VerticesCommands
{
    internal sealed class IntermediateToReplaceCommands : BaseVerticesCommands
    {
        public IntermediateToReplaceCommands(BasePathfindingRange endPoints) : base(endPoints)
        {
        }

        protected override IEnumerable<IVertexCommand> GetCommands(BasePathfindingRange endPoints)
        {
            yield return new CancelMarkToReplaceCommand(endPoints);
            yield return new MarkToReplaceCommand(endPoints);
        }

        protected override IEnumerable<IUndoCommand> GetUndoCommands(BasePathfindingRange endPoints)
        {
            yield return new UndoMarkToReplaceCommand(endPoints);
        }
    }
}