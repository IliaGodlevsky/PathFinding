using Commands.Interfaces;
using GraphLib.Base.EndPoints.Commands.EndPointsCommands;
using GraphLib.Base.EndPoints.Commands.UndoCommands;
using GraphLib.Interfaces;
using System.Collections.Generic;

namespace GraphLib.Base.EndPoints.Commands.VerticesCommands
{
    internal sealed class SetEndPointsCommands : BaseVerticesCommands
    {
        protected override IReadOnlyCollection<IUndoCommand> UndoCommands { get; }

        public SetEndPointsCommands(BaseEndPoints endPoints) : base(endPoints)
        {
            UndoCommands = new IUndoCommand[]
            {
                new UndoSetIntermediatesCommand(endPoints),
                new UndoSetTargetCommand(endPoints),
                new UndoSetSourceCommand(endPoints)
            };
        }

        protected override IReadOnlyCollection<IVertexCommand> GetCommands(BaseEndPoints endPoints)
        {
            return new IVertexCommand[]
            {
                new UnsetIntermediateCommand(endPoints),
                new UnsetTargetCommand(endPoints),
                new UnsetSourceCommand(endPoints),
                new SetSourceCommand(endPoints),
                new SetTargetCommand(endPoints),
                new SetIntermediateCommand(endPoints),
                new ReplaceIntermediateCommand(endPoints),
                new ReplaceIntermediateIsolatedCommand(endPoints),
                new ReplaceIsolatedSourceCommand(endPoints),
                new ReplaceIsolatedTargetCommand(endPoints),
            };
        }
    }
}