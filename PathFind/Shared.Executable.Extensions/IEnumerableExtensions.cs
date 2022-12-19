using Shared.Extensions;
using System.Collections.Generic;

namespace Shared.Executable.Extensions
{
    public static class IEnumerableExtensions
    {
        public static void Undo(this IEnumerable<IUndo> commands)
        {
            commands.ForEach(undo => undo.Undo());
        }
    }
}
