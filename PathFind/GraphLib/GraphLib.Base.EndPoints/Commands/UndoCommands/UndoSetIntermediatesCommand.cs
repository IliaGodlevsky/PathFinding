using Commands.Extensions;
using Commands.Interfaces;
using GraphLib.Base.EndPoints.BaseCommands;
using GraphLib.Base.EndPoints.Commands.EndPointsCommands;
using GraphLib.Interfaces;
using System.Linq;

namespace GraphLib.Base.EndPoints.Commands.UndoCommands
{
    internal sealed class UndoSetIntermediatesCommand : BaseIntermediatesUndoCommand
    {
        private readonly IExecutable<IVertex> unsetIntermediatesCommand;

        public UndoSetIntermediatesCommand(BaseEndPoints endPoints) : base(endPoints)
        {
            unsetIntermediatesCommand = new UnsetIntermediateCommand(endPoints);
        }

        public override void Undo()
        {
            unsetIntermediatesCommand.ExecuteForEach(Intermediates.ToArray());
        }
    }
}