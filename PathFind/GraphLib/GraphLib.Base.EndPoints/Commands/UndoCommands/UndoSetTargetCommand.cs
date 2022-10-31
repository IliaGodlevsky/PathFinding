using Commands.Interfaces;
using GraphLib.Base.EndPoints.BaseCommands;
using GraphLib.Base.EndPoints.Commands.EndPointsCommands;
using GraphLib.Interfaces;

namespace GraphLib.Base.EndPoints.Commands.UndoCommands
{
    internal sealed class UndoSetTargetCommand<TVertex> : BaseEndPointsUndoCommand<TVertex>
        where TVertex : IVertex, IVisualizable
    {
        private readonly IExecutable<TVertex> unsetTargetCommand;

        public UndoSetTargetCommand(BaseEndPoints<TVertex> endPoints) : base(endPoints)
        {
            unsetTargetCommand = new UnsetTargetCommand<TVertex>(endPoints);
        }

        public override void Undo()
        {
            unsetTargetCommand.Execute(Target);
        }
    }
}