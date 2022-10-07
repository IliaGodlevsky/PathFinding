using ConsoleVersion.Delegates;
using ConsoleVersion.Interface;

namespace ConsoleVersion.Commands
{
    internal class MenuCommand : IMenuCommand
    {
        private readonly string header;
        protected readonly Command command;

        public MenuCommand(string header, Command command)
        {
            this.header = header;
            this.command = command;
        }

        public virtual void Execute() => command.Invoke();

        public override string ToString() => header;
    }
}