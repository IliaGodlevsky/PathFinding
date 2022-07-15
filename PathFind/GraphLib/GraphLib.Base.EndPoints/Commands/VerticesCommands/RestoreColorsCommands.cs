using Commands.Interfaces;
using GraphLib.Base.EndPoints.Commands.EndPointsCommands;
using GraphLib.Interfaces;
using System;
using System.Collections.Generic;

namespace GraphLib.Base.EndPoints.Commands.VerticesCommands
{
    internal sealed class RestoreColorsCommands : BaseVerticesCommands
    {

        protected override IReadOnlyCollection<IUndoCommand> UndoCommands => Array.Empty<IUndoCommand>();

        public RestoreColorsCommands(BaseEndPoints endPoints) : base(endPoints)
        {

        }

        protected override IEnumerable<IVertexCommand> GetCommands(BaseEndPoints endPoints)
        {
            yield return new RestoreIntermediateColorCommand(endPoints);
            yield return new RestoreMarkedToReplaceColorCommand(endPoints);
            yield return new RestoreSourceColorCommand(endPoints);
            yield return new RestoreTargetColorCommand(endPoints);
        }
    }
}
