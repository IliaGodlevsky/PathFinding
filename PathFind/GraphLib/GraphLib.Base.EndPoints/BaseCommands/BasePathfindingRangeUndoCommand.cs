using Commands.Interfaces;
using GraphLib.Interfaces;

namespace GraphLib.Base.EndPoints.BaseCommands
{
    internal abstract class BasePathfindingRangeUndoCommand : IUndoCommand
    {
        protected readonly BasePathfindingRange range;

        protected IVertex Source => range.Source;

        protected IVertex Target => range.Target;

        protected BasePathfindingRangeUndoCommand(BasePathfindingRange range)
        {
            this.range = range;
        }

        public abstract void Undo();
    }
}