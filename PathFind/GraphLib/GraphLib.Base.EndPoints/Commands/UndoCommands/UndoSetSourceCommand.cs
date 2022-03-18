using Commands.Interfaces;
using Common.Attrbiutes;
using GraphLib.Base.EndPoints.BaseCommands;
using GraphLib.Base.EndPoints.Commands.VerticesCommands;
using GraphLib.Base.VertexCondition.EndPointsConditions;
using GraphLib.Interfaces;

namespace GraphLib.Base.EndPoints.Commands.UndoCommands
{
    [AttachedTo(typeof(SetEndPointsCommands))]
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