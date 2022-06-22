using ConsoleVersion.Interface;
using System;
using System.Linq;

namespace ConsoleVersion.Commands
{
    internal sealed class MenuCommand : IMenuCommand
    {
        private readonly Action action;
        private readonly Func<bool> validation;
        private readonly string header;

        public MenuCommand(string header, Action action, Func<bool> validation)
        {
            this.header = header;
            this.action = action;
            this.validation = validation;
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