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
    [AttachedTo(typeof(SetEndPointsCommands))]
    internal sealed class UndoSetIntermediatesCommand : BaseIntermediatesUndoCommand
    {
        public UndoSetIntermediatesCommand(BaseEndPoints endPoints) : base(endPoints)
        {
            unsetIntermediatesCommand = new UnsetIntermediateCommand(endPoints);
        }

        public override void Undo()
        {
            unsetIntermediatesCommand.ExecuteForEach(Intermediates.ToArray());
        }

        private readonly IExecutable<IVertex> unsetIntermediatesCommand;
    }
}
