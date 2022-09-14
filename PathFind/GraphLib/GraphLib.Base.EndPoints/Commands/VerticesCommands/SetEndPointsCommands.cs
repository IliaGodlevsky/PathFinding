using Commands.Interfaces;
using GraphLib.Base.EndPoints.Commands.EndPointsCommands;
using GraphLib.Base.EndPoints.Commands.UndoCommands;
using GraphLib.Interfaces;
using System.Collections.Generic;

namespace GraphLib.Base.EndPoints.Commands.VerticesCommands
{
    internal sealed class SetEndPointsCommands : BaseVerticesCommands
    {
        public SetEndPointsCommands(BaseEndPoints endPoints) : base(endPoints)
        {
        }

        protected override IEnumerable<IVertexCommand> GetCommands(BaseEndPoints endPoints)
        {
            yield return new UnsetIntermediateCommand(endPoints);
            yield return new UnsetTargetCommand(endPoints);
            yield return new UnsetSourceCommand(endPoints);
            yield return new SetSourceCommand(endPoints);
            yield return new SetTargetCommand(endPoints);
            yield return new SetIntermediateCommand(endPoints);
            yield return new ReplaceIntermediateCommand(endPoints);
            yield return new ReplaceIntermediateIsolatedCommand(endPoints);
            yield return new ReplaceIsolatedSourceCommand(endPoints);
            yield return new ReplaceIsolatedTargetCommand(endPoints);
        }

        protected override IEnumerable<IUndoCommand> GetUndoCommand(BaseEndPoints endPoints)
        {
            yield return new UndoSetIntermediatesCommand(endPoints);
            yield return new UndoSetTargetCommand(endPoints);
            yield return new UndoSetSourceCommand(endPoints);
        }
    }
}