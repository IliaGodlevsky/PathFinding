using GraphLib.Extensions;
using GraphLib.Interfaces;

namespace GraphLib.Base.EndPoints.BaseCommands
{
    internal abstract class BaseEndPointsCommand : IVertexCommand
    {
        protected IVisualizable Source => endPoints.Source.AsVisualizable();
        protected IVisualizable Target => endPoints.Target.AsVisualizable();

        protected readonly BaseEndPoints endPoints;

        protected BaseEndPointsCommand(BaseEndPoints endPoints)
        {
            this.endPoints = endPoints;
        }

        public abstract void Execute(IVertex vertex);

        public abstract bool CanExecute(IVertex vertex);        
    }
}