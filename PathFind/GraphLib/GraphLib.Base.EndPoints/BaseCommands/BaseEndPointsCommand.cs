using GraphLib.Extensions;
using GraphLib.Interfaces;

namespace GraphLib.Base.EndPoints.BaseCommands
{
    internal abstract class BaseEndPointsCommand : IVertexCommand
    {
        protected IVisualizable Source => endPoints.Source.AsVisualizable();
        protected IVisualizable Target => endPoints.Target.AsVisualizable();

        protected BaseEndPointsCommand(BaseEndPoints endPoints)
        {
            this.endPoints = endPoints;
        }

        protected readonly BaseEndPoints endPoints;

        public abstract bool IsTrue(IVertex vertex);

        public abstract void Execute(IVertex vertex);
    }
}
