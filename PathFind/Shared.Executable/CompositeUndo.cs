using Shared.Extensions;
using System.Collections.Generic;

namespace Shared.Executable
{
    public sealed class CompositeUndo : IUndo
    {
        private readonly IEnumerable<IUndo> commands;

        public CompositeUndo(params IUndo[] commands)
        {
            this.commands = commands;
        }

        public void Undo()
        {
            commands.ForEach(command=>command.Undo());
        }
    }
}