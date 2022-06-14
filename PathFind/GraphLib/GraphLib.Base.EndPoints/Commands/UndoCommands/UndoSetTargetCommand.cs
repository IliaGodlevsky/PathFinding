using Commands.Interfaces;
using GraphLib.Base.EndPoints.BaseCommands;
using GraphLib.Base.EndPoints.Commands.EndPointsCommands;
using GraphLib.Interfaces;

namespace GraphLib.Base.EndPoints.Commands.UndoCommands
{
    internal sealed class UndoSetTargetCommand : BaseEndPointsUndoCommand
    {
        private readonly IExecutable<IVertex> unsetTargetCommand;

        public UndoSetTargetCommand(BaseEndPoints endPoints) : base(endPoints)
        {
            unsetTargetCommand = new UnsetTargetCommand(endPoints);
        }

        public override void Undo()
        {
            unsetTargetCommand.Execute(Target);
        }
    }
}