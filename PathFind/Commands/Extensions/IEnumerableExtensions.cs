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
        public static void Execute<TCommand, TExecute, TCheck>(this IEnumerable<TCommand> commands, 
            TCheck validate, TExecute execute)
            where TCommand : IExecutable<TExecute>, IExecutionCheck<TCheck>
        {
            commands.FirstOrNullExecutable<TCommand, TCheck, TExecute>(command => command.CanExecute(validate)).Execute(execute);
        }

        public static void Execute<TCommand, TExecute>(this IEnumerable<TCommand> commands, TExecute execute)
            where TCommand : IExecutable<TExecute>, IExecutionCheck<TExecute>
        {
            commands.Execute(execute, execute);
        }

        public static IExecutable<TExecute> FirstOrNullExecutable<TCommand, TCheck, TExecute>(this IEnumerable<TCommand> commands, 
            Func<TCommand, bool> predicate)
            where TCommand : IExecutable<TExecute>, IExecutionCheck<TCheck>
        {
            return commands.FirstOrDefault(predicate) ?? NullExecutable<TExecute>.Instance;
        }

        public static void UndoAll(this IEnumerable<IUndoCommand> commands)
        {
            commands.ForEach(command => command.Undo());
        }
    }
}
