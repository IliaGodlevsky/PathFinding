using Common.Interface;
using GraphLib.Interfaces;
using System.Collections.ObjectModel;

namespace GraphLib.Base.EndPoints.BaseCommands
{
    internal abstract class BaseUndoCommand : IUndoCommand
    {
        protected IVertex Source => endPoints.Source;
        protected IVertex Target => endPoints.Target;
        protected Collection<IVertex> Intermediates => endPoints.Intermediates;
        protected Collection<IVertex> MarkedToReplace => endPoints.MarkedToReplace;

        protected BaseUndoCommand(BaseEndPoints endPoints)
        {
            this.endPoints = endPoints;
        }

        public abstract void Undo();

        protected readonly BaseEndPoints endPoints;
    }
}
