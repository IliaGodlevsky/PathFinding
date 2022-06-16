using Commands.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Commands.Extensions
{
    public static class IEnumerableExtensions
    {
        public static void ExecuteFirst<TCommand, TExecute, TCheck>(this IEnumerable<TCommand> commands, TCheck validate, TExecute execute)
            where TCommand : IExecutable<TExecute>, IExecutionCheck<TCheck>
        {
            commands.FirstOrDefault(command => command.CanExecute(validate))?.Execute(execute);
        }

        public static void ExecuteFirst<TCommand, TExecute>(this IEnumerable<TCommand> commands, TExecute execute)
            where TCommand : IExecutable<TExecute>, IExecutionCheck<TExecute>
        {
            commands.ExecuteFirst(execute, execute);
        }
    }
}
