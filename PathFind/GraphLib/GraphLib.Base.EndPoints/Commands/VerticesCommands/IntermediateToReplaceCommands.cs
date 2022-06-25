using Commands.Interfaces;
using GraphLib.Base.EndPoints.Commands.EndPointsCommands;
using GraphLib.Base.EndPoints.Commands.UndoCommands;
using GraphLib.Interfaces;
using System.Collections.Generic;

namespace GraphLib.Base.EndPoints.Commands.VerticesCommands
{
    internal sealed class IntermediateToReplaceCommands : BaseVerticesCommands
    {
        protected override IReadOnlyCollection<IUndoCommand> UndoCommands { get; }

        public IntermediateToReplaceCommands(BaseEndPoints endPoints) : base(endPoints)
        {
            UndoCommands = new IUndoCommand[] { new UndoMarkToReplaceCommand(endPoints) };
        }

        protected override IEnumerable<IVertexCommand> GetCommands(BaseEndPoints endPoints)
        {
            yield return new CancelMarkToReplaceCommand(endPoints);
            yield return new MarkToReplaceCommand(endPoints);
        }
    }
}