using Commands.Interfaces;
using GraphLib.Base.EndPoints.BaseCommands;
using GraphLib.Base.EndPoints.Commands.EndPointsCommands;
using GraphLib.Interfaces;

namespace GraphLib.Base.EndPoints.Commands.UndoCommands
{
    internal sealed class UndoSetSourceCommand : BaseEndPointsUndoCommand
    {
        private readonly IExecutable<IVertex> unsetSourceCommand;

        public UndoSetSourceCommand(BaseEndPoints endPoints) : base(endPoints)
        {
            unsetSourceCommand = new UnsetSourceCommand(endPoints);
        }

        public override void Undo()
        {
            unsetSourceCommand.Execute(Source);
        }
    }
}