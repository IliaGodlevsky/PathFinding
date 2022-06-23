using ConsoleVersion.Interface;
using System;
using System.Linq;

namespace ConsoleVersion.Commands
{
    internal sealed class MenuCommand : IMenuCommand
    {
        private readonly string header;
        private readonly Action action;
        private readonly Func<bool> validation;
        private readonly Action route;

        public MenuCommand(string header, Action action, Func<bool> validation, Action route = null)
        {
            this.header = header;
            this.action = action;
            this.validation = validation;
            this.route = route;
        }

        public void Execute()
        {
            if (CanExecute())
            {
                action.Invoke();
            }
            else if(route != null)
            {
                route.Invoke();
            }
        }

        private bool CanExecute()
        {
            return validation == null || validation
                .GetInvocationList()
                .Cast<Func<bool>>()
                .All(p => p.Invoke());
        }

        public override string ToString()
        {
            return header;
        }
    }
}