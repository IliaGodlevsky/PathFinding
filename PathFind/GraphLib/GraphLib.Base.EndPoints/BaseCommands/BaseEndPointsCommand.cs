using GraphLib.Interfaces;

namespace GraphLib.Base.EndPoints.BaseCommands
{
    internal abstract class BaseEndPointsCommand<TVertex> : IVertexCommand<TVertex>
        where TVertex : IVertex, IVisualizable
    {
        protected TVertex Source => endPoints.Source;

        protected TVertex Target => endPoints.Target;

        protected readonly BaseEndPoints<TVertex> endPoints;

        protected BaseEndPointsCommand(BaseEndPoints<TVertex> endPoints)
        {
            this.endPoints = endPoints;
        }

        public abstract void Execute(TVertex vertex);

        public abstract bool CanExecute(TVertex vertex);
    }
}