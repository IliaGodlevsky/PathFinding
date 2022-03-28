using ConsoleVersion.Interface;
using System;
using System.Linq;

namespace ConsoleVersion.Commands
{
    internal sealed class MenuCommand : IMenuCommand
    {
        private readonly Action action;
        private readonly Func<bool>[] predicates;

        public MenuCommand(Action action, params Func<bool>[] predicates)
        {
            this.action = action;
            this.predicates = predicates;
        }

        public void Execute()
        {
            if (CanExecute())
            {
                action.Invoke();
            }
        }

        private bool CanExecute()
        {
            return predicates.All(p => p is null || p.Invoke());
        }
    }
}