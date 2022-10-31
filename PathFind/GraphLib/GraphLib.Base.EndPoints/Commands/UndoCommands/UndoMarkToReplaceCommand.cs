using Commands.Extensions;
using Commands.Interfaces;
using GraphLib.Base.EndPoints.BaseCommands;
using GraphLib.Base.EndPoints.Commands.EndPointsCommands;
using GraphLib.Interfaces;
using System.Linq;

namespace GraphLib.Base.EndPoints.Commands.UndoCommands
{
    internal sealed class UndoMarkToReplaceCommand<TVertex> : BaseIntermediatesUndoCommand<TVertex>
        where TVertex : IVertex, IVisualizable
    {
        private readonly IExecutable<TVertex> cancelMarkToReplaceCommand;

        public UndoMarkToReplaceCommand(BaseEndPoints<TVertex> endPoints) : base(endPoints)
        {
            cancelMarkToReplaceCommand = new CancelMarkToReplaceCommand<TVertex>(endPoints);
        }

        public override void Undo()
        {
            cancelMarkToReplaceCommand.Execute(MarkedToReplace.ToArray());
        }
    }
}