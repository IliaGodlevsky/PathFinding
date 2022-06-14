using Commands.Extensions;
using Commands.Interfaces;
using Common.Extensions.EnumerableExtensions;
using GraphLib.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace GraphLib.Base.EndPoints.Commands.VerticesCommands
{
    internal abstract class BaseVerticesCommands : IVerticesCommands
    {
        protected IReadOnlyCollection<IVertexCommand> ExecuteCommands { get; }

        protected abstract IReadOnlyCollection<IUndoCommand> UndoCommands { get; }

        protected BaseVerticesCommands(BaseEndPoints endPoints)
        {
            ExecuteCommands = GetCommands(endPoints)
                .OrderByOrderAttribute()
                .ToArray();
        }

        public void Execute(IVertex vertex)
        {
            ExecuteCommands.ExecuteFirst(vertex);
        }

        public void Undo()
        {
            UndoCommands.UndoAll();
        }

        protected abstract IReadOnlyCollection<IVertexCommand> GetCommands(BaseEndPoints endPoints);
    }
}