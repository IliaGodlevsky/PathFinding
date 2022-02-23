using Common.Extensions;
using Common.Interface;
using GraphLib.Base.EndPoints.Attributes;
using GraphLib.Base.EndPoints.BaseCommands;
using GraphLib.Base.EndPoints.Commands.EndPointsCommands;
using GraphLib.Base.EndPoints.Commands.VerticesCommands;
using GraphLib.Interfaces;
using System.Linq;

namespace GraphLib.Base.EndPoints.Commands.UndoCommands
{
    [AttachedTo(typeof(IntermediateToReplaceCommands))]
    internal sealed class UndoMarkToReplaceCommand : BaseIntermediatesUndoCommand
    {
        public UndoMarkToReplaceCommand(BaseEndPoints endPoints) : base(endPoints)
        {
            cancelMarkToReplaceCommand = new CancelMarkToReplaceCommand(endPoints);
        }

        public override void Undo()
        {
            cancelMarkToReplaceCommand.ExecuteForEach(MarkedToReplace.ToArray());
        }

        private readonly IExecutable<IVertex> cancelMarkToReplaceCommand;
    }
}
