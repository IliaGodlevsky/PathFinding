using System;
using System.Collections.Generic;
using System.Linq;

namespace Common
{
    public class Command<T>
    {
        public static void ExecuteFirstExecutable(
            IEnumerable<Command<T>> commands, T parametre)
        {
            bool IsExecutable(Command<T> command)
                => command?.CanExecute(parametre) == true;

            commands
                ?.FirstOrDefault(IsExecutable)
                ?.Execute(parametre);
        }

        public Command(Predicate<T> canExecute = null,
            Action<T> execute = null)
        {
            this.canExecute = canExecute;
            this.execute = execute;
        }

        public bool? CanExecute(T parametre)
        {
            return canExecute == null
                || canExecute?.Invoke(parametre) == true;
        }

        public void Execute(T paramtre)
        {
            execute?.Invoke(paramtre);
        }

        private readonly Predicate<T> canExecute;
        private readonly Action<T> execute;
    }
}
