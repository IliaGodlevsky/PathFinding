using GraphLib.Base.EndPoints.Extensions;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using GraphLib.NullRealizations;
using System.Collections.Generic;

namespace GraphLib.Base.EndPoints.Commands.VerticesCommands
{
    internal abstract class BaseVerticesCommands : IVerticesCommands
    {
        protected IReadOnlyCollection<IVertex> Intermediates => endPoints.Intermediates;
        protected IReadOnlyCollection<IVertex> MarkedToReplace => endPoints.MarkedToReplace;

        protected IReadOnlyList<IVertexCommand> Commands { get; }

        protected BaseVerticesCommands(BaseEndPoints endPoints)
        {
            this.endPoints = endPoints;
            Commands = this.GetAttachedCommands(endPoints);
        }

        public virtual void Execute(IVertex vertex)
        {
            Commands.Execute(vertex ?? NullVertex.Instance);
        }

        public abstract void Undo();

        protected readonly BaseEndPoints endPoints;
    }
}