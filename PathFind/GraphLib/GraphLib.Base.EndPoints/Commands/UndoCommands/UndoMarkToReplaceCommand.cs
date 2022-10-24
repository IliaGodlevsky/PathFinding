using Commands.Extensions;
using Commands.Interfaces;
using GraphLib.Base.EndPoints.BaseCommands;
using GraphLib.Base.EndPoints.Commands.EndPointsCommands;
using GraphLib.Interfaces;
using System.Linq;

namespace GraphLib.Base.EndPoints.Commands.UndoCommands
{
    internal sealed class UndoMarkToReplaceCommand : BaseIntermediatesUndoCommand
    {
        private readonly IExecutable<IVertex> cancelMarkToReplaceCommand;

        public UndoMarkToReplaceCommand(BasePathfindingRange endPoints) : base(endPoints)
        {
            cancelMarkToReplaceCommand = new CancelMarkToReplaceCommand(endPoints);
        }

        public override void Undo()
        {
            cancelMarkToReplaceCommand.Execute(MarkedToReplace.ToArray());
        }
    }
}