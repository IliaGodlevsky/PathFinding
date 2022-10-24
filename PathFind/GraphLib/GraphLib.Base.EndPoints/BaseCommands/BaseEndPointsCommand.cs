using GraphLib.Extensions;
using GraphLib.Interfaces;

namespace GraphLib.Base.EndPoints.BaseCommands
{
    internal abstract class BasePathfindingRangeCommand : IVertexCommand
    {
        protected IVisualizable Source => range.Source.AsVisualizable();

        protected IVisualizable Target => range.Target.AsVisualizable();

        protected readonly BasePathfindingRange range;

        protected BasePathfindingRangeCommand(BasePathfindingRange range)
        {
            this.range = range;
        }

        public abstract void Execute(IVertex vertex);

        public abstract bool CanExecute(IVertex vertex);
    }
}