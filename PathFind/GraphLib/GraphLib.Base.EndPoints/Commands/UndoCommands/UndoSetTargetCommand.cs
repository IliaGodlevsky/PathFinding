using Commands.Interfaces;
using GraphLib.Base.EndPoints.BaseCommands;
using GraphLib.Base.EndPoints.Commands.EndPointsCommands;
using GraphLib.Interfaces;

namespace GraphLib.Base.EndPoints.Commands.UndoCommands
{
    internal sealed class UndoSetTargetCommand : BasePathfindingRangeUndoCommand
    {
        private readonly IExecutable<IVertex> unsetTargetCommand;

        public UndoSetTargetCommand(BasePathfindingRange endPoints) : base(endPoints)
        {
            unsetTargetCommand = new UnsetTargetCommand(endPoints);
        }

        public override void Undo()
        {
            unsetTargetCommand.Execute(Target);
        }
    }
}