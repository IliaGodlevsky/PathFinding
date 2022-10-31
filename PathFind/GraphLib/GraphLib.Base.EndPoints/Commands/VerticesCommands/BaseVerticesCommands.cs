using Commands.Extensions;
using Commands.Interfaces;
using Common.Extensions.EnumerableExtensions;
using GraphLib.Interfaces;
using System.Collections.Generic;

namespace GraphLib.Base.EndPoints.Commands.VerticesCommands
{
    internal abstract class BaseVerticesCommands<TVertex> : IVerticesCommands<TVertex>
        where TVertex : IVertex, IVisualizable
    {
        protected IEnumerable<IVertexCommand<TVertex>> ExecuteCommands { get; }

        protected IEnumerable<IUndoCommand> UndoCommands { get; }

        protected BaseVerticesCommands(BaseEndPoints<TVertex> endPoints)
        {
            ExecuteCommands = GetCommands(endPoints).OrderByOrderAttribute().ToReadOnly();
            UndoCommands = GetUndoCommands(endPoints).ToReadOnly();
        }

        public void Execute(TVertex vertex)
        {
            ExecuteCommands.ExecuteFirst(vertex);
        }

        public void Undo()
        {
            UndoCommands.ForEach(command => command.Undo());
        }

        protected abstract IEnumerable<IVertexCommand<TVertex>> GetCommands(BaseEndPoints<TVertex> endPoints);

        protected abstract IEnumerable<IUndoCommand> GetUndoCommands(BaseEndPoints<TVertex> endPoints);
    }
}