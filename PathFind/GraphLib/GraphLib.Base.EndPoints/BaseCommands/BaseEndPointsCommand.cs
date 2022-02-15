using GraphLib.Interfaces;

namespace GraphLib.Base.EndPoints.BaseCommands
{
    internal abstract class BaseEndPointsCommand : IVertexCommand
    {
        protected BaseEndPointsCommand(BaseEndPoints endPoints)
        {
            this.endPoints = endPoints;
        }

        protected readonly BaseEndPoints endPoints;

        public abstract bool IsTrue(IVertex vertex);

        public abstract void Execute(IVertex vertex);
    }
}
