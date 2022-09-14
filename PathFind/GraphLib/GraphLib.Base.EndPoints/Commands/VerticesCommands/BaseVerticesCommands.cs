using Commands.Extensions;
using Commands.Interfaces;
using Common.Extensions.EnumerableExtensions;
using GraphLib.Interfaces;
using GraphLib.NullRealizations;
using System.Collections.Generic;

namespace GraphLib.Base.EndPoints.Commands.VerticesCommands
{
    internal abstract class BaseVerticesCommands : IVerticesCommands
    {
        protected IEnumerable<IVertexCommand> ExecuteCommands { get; }

        protected IEnumerable<IUndoCommand> UndoCommands { get; }

        protected BaseVerticesCommands(BaseEndPoints endPoints)
        {
            ExecuteCommands = GetCommands(endPoints).OrderByOrderAttribute().ToReadOnly();
            UndoCommands = GetUndoCommand(endPoints).ToReadOnly();
        }

        public void Execute(IVertex vertex)
        {
            ExecuteCommands.ExecuteFirst(vertex ?? NullVertex.Instance);
        }

        public void Undo()
        {
            UndoCommands.ForEach(command => command.Undo());
        }

        protected abstract IEnumerable<IVertexCommand> GetCommands(BaseEndPoints endPoints);

        protected abstract IEnumerable<IUndoCommand> GetUndoCommand(BaseEndPoints endPoints);
    }
}