using Shared.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace Shared.Executable.Extensions
{
    public static class IEnumerableExtensions
    {
        public static void Execute<TCommand, TExecute, TCheck>(this IEnumerable<TCommand> commands, 
            TCheck validate, TExecute execute)
            where TCommand : IExecutable<TExecute>, IExecutionCheck<TCheck>
        {
            commands.FirstOrDefault(command => command.CanExecute(validate))?.Execute(execute);
        }

        public static void Execute<TCommand, TExecute>(this IEnumerable<TCommand> commands, TExecute execute)
            where TCommand : IExecutable<TExecute>, IExecutionCheck<TExecute>
        {
            commands.Execute(execute, execute);
        }

        public static void Undo(this IEnumerable<IUndo> commands)
        {
            commands.ForEach(undo => undo.Undo());
        }
    }
}
