using Commands.Interfaces;
using GraphLib.Interfaces;

namespace GraphLib.Base.EndPoints.BaseCommands
{
    internal abstract class BaseEndPointsUndoCommand<TVertex> : IUndoCommand
        where TVertex : IVertex, IVisualizable
    {
        protected readonly BaseEndPoints<TVertex> endPoints;

        protected TVertex Source => endPoints.Source;

        protected TVertex Target => endPoints.Target;

        protected BaseEndPointsUndoCommand(BaseEndPoints<TVertex> endPoints)
        {
            this.endPoints = endPoints;
        }

        public abstract void Undo();
    }
}