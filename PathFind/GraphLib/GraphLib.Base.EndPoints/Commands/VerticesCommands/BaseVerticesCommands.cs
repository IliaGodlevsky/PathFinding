using GraphLib.Base.EndPoints.Extensions;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using System.Collections.Generic;

namespace GraphLib.Base.EndPoints.Commands.VerticesCommands
{
    internal abstract class BaseVerticesCommands : IVerticesCommands
    {
        protected IReadOnlyList<IVertexCommand> Commands { get; }

        protected BaseVerticesCommands(BaseEndPoints endPoints)
        {
            this.endPoints = endPoints;
            Commands = this.GetAttachedCommands(endPoints);
        }

        public virtual void Execute(IVertex vertex)
        {
            if (!vertex.IsIsolated())
            {
                Commands.Execute(vertex);
            }
        }

        public abstract void Reset();

        protected readonly BaseEndPoints endPoints;
    }
}
