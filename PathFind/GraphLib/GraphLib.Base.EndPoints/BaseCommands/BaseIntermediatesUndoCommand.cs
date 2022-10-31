using GraphLib.Interfaces;
using System.Collections.ObjectModel;

namespace GraphLib.Base.EndPoints.BaseCommands
{
    internal abstract class BaseIntermediatesUndoCommand<TVertex> : BaseEndPointsUndoCommand<TVertex>
        where TVertex : IVertex, IVisualizable
    {
        protected Collection<TVertex> Intermediates => endPoints.Intermediates;

        protected Collection<TVertex> MarkedToReplace => endPoints.MarkedToReplace;

        protected BaseIntermediatesUndoCommand(BaseEndPoints<TVertex> endPoints) : base(endPoints)
        {

        }
    }
}