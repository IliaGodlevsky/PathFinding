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

        protected override IReadOnlyCollection<IVertexCommand> GetCommands(BaseEndPoints endPoints)
        {
            return new IVertexCommand[]
            {
                new CancelMarkToReplaceCommand(endPoints),
                new MarkToReplaceCommand(endPoints)
            };
        }
    }
}