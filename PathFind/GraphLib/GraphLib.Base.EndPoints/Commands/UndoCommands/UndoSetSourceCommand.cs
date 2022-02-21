using Common.Interface;
using GraphLib.Base.EndPoints.Attributes;
using GraphLib.Base.EndPoints.BaseCommands;
using GraphLib.Base.EndPoints.Commands.VerticesCommands;
using GraphLib.Base.VertexCondition.EndPointsConditions;
using GraphLib.Interfaces;

namespace GraphLib.Base.EndPoints.Commands.UndoCommands
{
    [AttachedTo(typeof(SetEndPointsCommands))]
    internal sealed class UndoSetSourceCommand : BaseUndoCommand
    {
        public UndoSetSourceCommand(BaseEndPoints endPoints) : base(endPoints)
        {
            unsetSourceCommand = new UnsetSourceCommand(endPoints);
        }

        public override void Undo()
        {
            unsetSourceCommand.Execute(Source);
        }

        private readonly IExecutable<IVertex> unsetSourceCommand;
    }
}
