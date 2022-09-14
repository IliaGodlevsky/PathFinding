using Commands.Interfaces;
using GraphLib.Base.EndPoints.Commands.EndPointsCommands;
using GraphLib.Base.EndPoints.Commands.UndoCommands;
using GraphLib.Interfaces;
using System.Collections.Generic;

namespace GraphLib.Base.EndPoints.Commands.VerticesCommands
{
    internal sealed class IntermediateToReplaceCommands : BaseVerticesCommands
    {
        public IntermediateToReplaceCommands(BaseEndPoints endPoints) : base(endPoints)
        {
        }

        protected override IEnumerable<IVertexCommand> GetCommands(BaseEndPoints endPoints)
        {
            yield return new CancelMarkToReplaceCommand(endPoints);
            yield return new MarkToReplaceCommand(endPoints);
        }

        protected override IEnumerable<IUndoCommand> GetUndoCommand(BaseEndPoints endPoints)
        {
            yield return new UndoMarkToReplaceCommand(endPoints);
        }
    }
}