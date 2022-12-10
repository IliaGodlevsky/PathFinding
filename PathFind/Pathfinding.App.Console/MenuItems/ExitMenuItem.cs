using Pathfinding.App.Console.Exceptions;
using Pathfinding.App.Console.Interface;

namespace Pathfinding.App.Console.MenuItems
{
    internal sealed class ExitMenuItem : IMenuItem
    {
        public int Order => int.MaxValue;

        public void Execute()
        {
            throw new ExitRequiredException();
        }

        bool IMenuItem.CanBeExecuted() => true;

        public override string ToString() => "Exit";
    }
}
