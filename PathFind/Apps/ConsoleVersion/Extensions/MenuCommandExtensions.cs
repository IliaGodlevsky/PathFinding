using ConsoleVersion.Commands;

namespace ConsoleVersion.Extensions
{
    internal static class IMenuCommandExtensions
    {
        public static void Invoke(this MenuCommand command)
        {
            if (command.CanExecute())
            {
                command.Execute();
            }
        }
    }
}