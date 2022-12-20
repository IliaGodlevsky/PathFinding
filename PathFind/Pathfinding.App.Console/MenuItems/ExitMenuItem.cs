using Pathfinding.App.Console.Exceptions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;

namespace Pathfinding.App.Console.MenuItems
{
    internal sealed class ExitMenuItem : IMenuItem
    {
        public void Execute()
        {
            throw new ExitRequiredException();
        }

        bool IMenuItem.CanBeExecuted() => true;

        public override string ToString()
        {
            return Languages.Exit;
        }
    }
}
