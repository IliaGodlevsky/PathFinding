using ConsoleVersion.Delegates;
using ConsoleVersion.Interface;

namespace ConsoleVersion.Commands
{
    internal sealed class MenuCommand : IMenuCommand
    {
        private readonly string header;
        private readonly Command command;

        public MenuCommand(string header, Command command)
        {
            this.header = header;
            this.command = command;
        }

        public void Execute()
        {
            command.Invoke();
        }

        public override string ToString()
        {
            return header;
        }
    }
}