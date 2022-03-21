using Commands.Interfaces;
using Commands.Realizations;
using Common.Extensions.EnumerableExtensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Commands.Extensions
{
    public static class IEnumerableExtensions
    {
        public static void ExecuteFirst<TCommand, TExecute, TCheck>(this IEnumerable<TCommand> commands, TCheck validate, TExecute execute)
            where TCommand : IExecutable<TExecute>, IExecutionCheck<TCheck>
        {
            commands.FirstOrNullExecutable<TCommand, TCheck, TExecute>(command => command.CanExecute(validate)).Execute(execute);
        }

        public static void ExecuteFirst<TCommand, TExecute>(this IEnumerable<TCommand> commands, TExecute execute)
            where TCommand : IExecutable<TExecute>, IExecutionCheck<TExecute>
        {
            commands.ExecuteFirst(execute, execute);
        }

        public static void ExecuteAll<TCommand, T>(this IEnumerable<TCommand> commands, T execute)
            where TCommand : IExecutable<T>
        {
            commands.ForEach(command => command.Execute(execute));
        }

        public static void UndoAll(this IEnumerable<IUndoCommand> commands)
        {
            commands.ForEach(command => command.Undo());
        }

        private static IExecutable<TExecute> FirstOrNullExecutable<TCommand, TCheck, TExecute>(this IEnumerable<TCommand> commands,
            Func<TCommand, bool> predicate)
            where TCommand : IExecutable<TExecute>, IExecutionCheck<TCheck>
        {
            return commands.FirstOrDefault(predicate) ?? NullExecutable<TExecute>.Instance;
        }
    }
}
