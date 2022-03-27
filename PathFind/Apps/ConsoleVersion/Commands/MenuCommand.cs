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

        public void Execute(params object[] args)
        {
            if (predicates.All(IsTrue))
            {
                action.Invoke();
            }
        }

        private static bool IsTrue(Func<bool> predicate)
        {
            return predicate == null || predicate.Invoke();
        }
    }
}