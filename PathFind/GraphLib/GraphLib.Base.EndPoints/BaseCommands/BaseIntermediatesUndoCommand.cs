using GraphLib.Interfaces;
using System.Collections.ObjectModel;

namespace GraphLib.Base.EndPoints.BaseCommands
{
    internal abstract class BaseIntermediatesUndoCommand : BasePathfindingRangeUndoCommand
    {
        protected Collection<IVertex> Intermediates => range.Intermediates;

        protected Collection<IVertex> MarkedToReplace => range.MarkedToReplace;

        protected BaseIntermediatesUndoCommand(BasePathfindingRange range) : base(range)
        {

        }
    }
}