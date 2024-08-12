using Pathfinding.Shared.Extensions;
using Pathfinding.Shared.Interface;
using System.Collections.Generic;

namespace Pathfinding.Shared
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
            commands.ForEach(command => command.Undo());
        }
    }
}