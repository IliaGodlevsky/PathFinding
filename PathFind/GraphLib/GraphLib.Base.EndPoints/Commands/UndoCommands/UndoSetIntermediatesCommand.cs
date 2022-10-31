using Commands.Extensions;
using Commands.Interfaces;
using GraphLib.Base.EndPoints.BaseCommands;
using GraphLib.Base.EndPoints.Commands.EndPointsCommands;
using GraphLib.Interfaces;
using System.Linq;

namespace GraphLib.Base.EndPoints.Commands.UndoCommands
{
    internal sealed class UndoSetIntermediatesCommand<TVertex> : BaseIntermediatesUndoCommand<TVertex>
        where TVertex : IVertex, IVisualizable
    {
        private readonly IExecutable<TVertex> unsetIntermediatesCommand;

        public UndoSetIntermediatesCommand(BaseEndPoints<TVertex> endPoints) : base(endPoints)
        {
            unsetIntermediatesCommand = new UnsetIntermediateCommand<TVertex>(endPoints);
        }

        public override void Undo()
        {
            unsetIntermediatesCommand.Execute(Intermediates.ToArray());
        }
    }
}