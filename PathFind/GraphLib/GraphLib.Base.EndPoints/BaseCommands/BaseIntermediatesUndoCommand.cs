using GraphLib.Interfaces;
using System.Collections.ObjectModel;

namespace GraphLib.Base.EndPoints.BaseCommands
{
    internal abstract class BaseIntermediatesUndoCommand : BaseEndPointsUndoCommand
    {
        protected Collection<IVertex> Intermediates => endPoints.Intermediates;
        protected Collection<IVertex> MarkedToReplace => endPoints.MarkedToReplace;

        protected BaseIntermediatesUndoCommand(BaseEndPoints endPoints) : base(endPoints)
        {

        }
    }
}