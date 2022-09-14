using Commands.Interfaces;
using GraphLib.Interfaces;

namespace GraphLib.Base.EndPoints.BaseCommands
{
    internal abstract class BaseEndPointsUndoCommand : IUndoCommand
    {
        protected readonly BaseEndPoints endPoints;

        protected IVertex Source => endPoints.Source;

        protected IVertex Target => endPoints.Target;

        protected BaseEndPointsUndoCommand(BaseEndPoints endPoints)
        {
            this.endPoints = endPoints;
        }

        public abstract void Undo();
    }
}