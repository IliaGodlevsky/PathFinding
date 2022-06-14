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

        protected override IReadOnlyCollection<IVertexCommand> GetCommands(BaseEndPoints endPoints)
        {
            return new IVertexCommand[]
            {
                new RestoreIntermediateColorCommand(endPoints),
                new RestoreMarkedToReplaceColorCommand(endPoints),
                new RestoreSourceColorCommand(endPoints),
                new RestoreTargetColorCommand(endPoints)
            };
        }
    }
}
