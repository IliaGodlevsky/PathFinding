using ConsoleVersion.Delegates;
using System.Linq;

namespace ConsoleVersion.Commands
{
    internal class ConditionedMenuCommand : MenuCommand
    {
        private readonly Condition condition;

        public ConditionedMenuCommand(string header, Command command, 
            Condition condition) : base(header, command)
        {
            this.condition = condition;
        }

        public override void Execute()
        {
            if (CanExecute())
            {
                command.Invoke();
            }
        }

        private bool CanExecute()
        {
            return condition
                .GetInvocationList()
                .Cast<Condition>()
                .All(p => p.Invoke());
        }
    }
}
