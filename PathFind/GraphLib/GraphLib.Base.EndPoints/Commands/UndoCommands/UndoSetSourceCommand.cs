using Commands.Interfaces;
using GraphLib.Base.EndPoints.BaseCommands;
using GraphLib.Base.EndPoints.Commands.EndPointsCommands;
using GraphLib.Interfaces;

namespace GraphLib.Base.EndPoints.Commands.UndoCommands
{
    internal sealed class UndoSetSourceCommand<TVertex> : BaseEndPointsUndoCommand<TVertex>
        where TVertex : IVertex, IVisualizable
    {
        private readonly IExecutable<TVertex> unsetSourceCommand;

        public UndoSetSourceCommand(BaseEndPoints<TVertex> endPoints) : base(endPoints)
        {
            unsetSourceCommand = new UnsetSourceCommand<TVertex>(endPoints);
        }

        public override void Undo()
        {
            unsetSourceCommand.Execute(Source);
        }
    }
}