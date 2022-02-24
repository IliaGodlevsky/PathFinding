using Commands.Extensions;
using Commands.Interfaces;
using GraphLib.Base.EndPoints.Extensions;
using GraphLib.Interfaces;
using System.Collections.Generic;

namespace GraphLib.Base.EndPoints.Commands.VerticesCommands
{
    internal abstract class BaseVerticesCommands : IVerticesCommands
    {
        private IReadOnlyCollection<IVertexCommand> ExecuteCommands { get; }
        private IReadOnlyCollection<IUndoCommand> UndoCommands { get; }

        protected BaseVerticesCommands(BaseEndPoints endPoints)
        {
            ExecuteCommands = this.GetAttachedExecuteCommands(endPoints);
            UndoCommands = this.GetAttachedUndoCommands(endPoints);
        }

        public void Execute(IVertex vertex)
        {
            ExecuteCommands.Execute(vertex);
        }

        public void Undo()
        {
            UndoCommands.UndoAll();
        }
    }
}