using ConsoleVersion.Delegates;
using ConsoleVersion.Interface;
using System.Linq;

namespace ConsoleVersion.Commands
{
    internal sealed class ConditionedMenuCommand : IMenuCommand
    {
        private readonly IMenuCommand command;
        private readonly Condition condition;

        public ConditionedMenuCommand(IMenuCommand command, Condition condition)
        {
            this.command = command;
            this.condition = condition;
        }

        public void Execute()
        {
            if (CanExecute())
            {
                command.Execute();
            }
        }

        public bool CanExecute()
        {
            return condition
                .GetInvocationList()
                .Cast<Condition>()
                .All(p => p.Invoke());
        }

        public override string ToString()
        {
            return command.ToString();
        }
    }
}
