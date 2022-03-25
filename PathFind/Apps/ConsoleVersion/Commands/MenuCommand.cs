using System;

namespace ConsoleVersion.Commands
{
    internal sealed class MenuCommand
    {
        private readonly Action action;
        private readonly Func<bool> predicate;

        public MenuCommand(Action action, Func<bool> predicate)
        {
            this.action = action;
            this.predicate = predicate;
        }

        public void Execute()
        {
            action.Invoke();
        }

        public bool CanExecute()
        {
            return predicate == null 
                || predicate.Invoke();
        }
    }
}
